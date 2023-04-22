using TaskList.Business.Abstract;
using TaskList.Business.Concrete;
using TaskList.DataAccess.Concrete;
using TaskList.Interfaces;

namespace TaskList.DataAccess
{
    public static class ServiceRegistrations
    {

        public static void AddDataAccesLayerService(this IServiceCollection services)
        {
            services.AddScoped<IUserDal,UserDal>();
            services.AddScoped<ITaskDal, TaskDal>();
            services.AddScoped<ICommentDal, CommentDal>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ITaskService, TaskManager>();
            services.AddScoped<ICommentService, CommentManager>();
        }
    }
}
