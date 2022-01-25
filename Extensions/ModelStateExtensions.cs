using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErros(this ModelStateDictionary modelState) =>
            (from item in modelState.Values from error in item.Errors select error.ErrorMessage).ToList();
        
    }
}
