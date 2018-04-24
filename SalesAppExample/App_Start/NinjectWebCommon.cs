[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SalesAppExample.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SalesAppExample.App_Start.NinjectWebCommon), "Stop")]

namespace SalesAppExample.App_Start
{
    using System;
    using System.Web;
    using System.Linq;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using SalesAppExample.Services.DiscountCalculator;
    using SalesAppExample.Factories;
    using Ninject.Extensions.Factory;
    using AutoMapper;
    using Entities;
    using ViewModels;
    using Models;
    using Ninject.Modules;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Discount factory enables dynamic binding for IDiscountCalculator interface implementations
            kernel.Bind<IDiscountCalculatorFactory>().ToFactory(() => new UseFirstArgumentAsNameInstanceProvider());
            kernel.Bind<IDiscountCalculator>().To<FirstPurchaseDiscountCalculator>().Named("First Purchase Discount");
            kernel.Bind<IDiscountCalculator>().To<BigPurchaseDiscountCalculator>().Named("Big Purchase Discount");
            kernel.Bind<IDiscountCalculator>().To<ValuedCustomerDiscountCalculator>().Named("Valued Customer Discount");

            //AutoMapper setup
            Mapper.Initialize(config =>
            {
                config.ConstructServicesUsing(t => kernel.Get(t));
                config.CreateMap<Customer, CustomerViewModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id));
                config.CreateMap<DiscountType, DiscountTypeViewModel>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id));
                config.CreateMap<CalculatorViewModel, DiscountCalculationModel>()
                    .ForMember(dest => dest.Units, opt => opt.MapFrom(x => x.Units))
                    .ForMember(dest => dest.Sum, opt => opt.MapFrom(x => x.Sum))
                    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(x => x.PickedCustomerId));
            });
        }        
    }

    public class UseFirstArgumentAsNameInstanceProvider : StandardInstanceProvider
    {
        protected override string GetName(System.Reflection.MethodInfo methodInfo, object[] arguments)
        {
            return arguments[0].ToString();
        }

        protected override Ninject.Parameters.IConstructorArgument[] GetConstructorArguments(System.Reflection.MethodInfo methodInfo, object[] arguments)
        {
            return base.GetConstructorArguments(methodInfo, arguments).Skip(1).ToArray();
        }
    }
}
