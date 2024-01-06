using Entity.Produtos.Data.Contexto;
using Entity.Pedidos.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Entity.Clientes.Domain.Repositories;
using Entity.Clientes.Data.Repositories;
using Entity.Produtos.Domain.Repositories;
using Entity.Produtos.Data.Repositories;
using Entity.Pedidos.Domain.Repositories;
using Entity.Pedidos.Data.Repositories;
using Entity.Shared.Mediator;
using Entity.Clientes.Application.Handlers;
using Entity.Produtos.Application.Handlers;
using Entity.Pedidos.Application.Handlers;

namespace entity_framework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var stringConexao = Configuration.GetConnectionString("ConnectionString");
            //services.AddDbContext<DbContexto>(options => options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));
            services.AddDbContext<ProdutosDbContexto>(options => options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));
            services.AddDbContext<Entity.Clientes.Domain.Entidades.ClienteDbContexto>(options => options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));
            services.AddDbContext<PedidoDbContexto>(options => options.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao)));
            //registrar serviços
            services.AddScoped<Entity.Clientes.Domain.Entidades.ClienteDbContexto>();
            services.AddScoped<ProdutosDbContexto>();
            services.AddScoped<PedidoDbContexto>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutosRepository, ProdutosRepository>();
            services.AddScoped<IFornecedoresRepository, FornecedoresRepository>();
            services.AddScoped<IPedidosRepository, PedidosRepository>();

            //eventos
            services.AddSingleton<IMediatorHandler, MediatorHandler>((provider) => 
            {
                var mediator = new MediatorHandler();
                mediator.RegistrarEventoHandler(new ClienteRegistradoEventoHandler());
                mediator.RegistrarEventoHandler(new ProdutosPedidosEventoHandler());
                mediator.RegistrarComandoHandler(new CadastrarPedidoHandler(provider));
                mediator.RegistrarComandoHandler(new AtualizarPedidoHandler(provider));
                mediator.RegistrarComandoHandler(new RemoverPedidoHandler(provider));
                return mediator;
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}