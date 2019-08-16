
using Microsoft.AspNetCore.Mvc.Filters;
using DynamicSerialization.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Collections.Generic;
using System;

namespace DynamicSerialization.Filters
{
    public class DynamicFieldsFilter : ActionFilterAttribute
    {
        private readonly Type targetType;

        public DynamicFieldsFilter(Type type) => this.targetType = type;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var fields = context.HttpContext.Request.Query["fields"];
            var jsonResolver = new DynamicFieldResolver();

            foreach (var field in fields)
            {
                string[] requestedFields = field.ToLower().Split(',');
                jsonResolver.IncludePropertyForSerialization(targetType, requestedFields);
            }

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;
            ((JsonResult)context.Result).SerializerSettings = serializerSettings;
        }
    }
}