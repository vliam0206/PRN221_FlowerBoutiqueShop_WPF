using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Interfaces;
using Repositories.UnitOfWork;
using System.Windows;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            // COnfig for DI
            ServiceCollection services = new ServiceCollection();
            ConfigurationService(services);
            _serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigurationService(ServiceCollection services)
        {
            services.AddSingleton(typeof(IAccountRepository), typeof(AccountRepository));
            services.AddSingleton(typeof(ICustomerRepository), typeof(CustomerRepository));            
            services.AddSingleton(typeof(IFlowerBouquetRepository), typeof(FlowerBouquetRepository));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
            services.AddSingleton(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddSingleton(typeof(ISuplierRepository), typeof(SupplierRepository));
            services.AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddSingleton<LoginWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var loginWindow = _serviceProvider.GetService<LoginWindow>();
            loginWindow.Show();
        }
    }
}
