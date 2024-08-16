// System

global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Linq;
global using System.Reflection;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Threading.Tasks;
// Microsoft
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
// ThirdParty
global using Furion;
global using Furion.ConfigurableOptions;
global using Furion.DatabaseAccessor;
global using Furion.DataValidation;
global using Furion.DependencyInjection;
global using Furion.DynamicApiController;
global using Furion.EventBus;
global using Furion.FriendlyException;
global using Furion.JsonSerialization;
global using Furion.UnifyResult;
global using SqlSugar;
global using Yitter.IdGenerator;
global using Mapster;
global using Newtonsoft.Json;
global using NewLife;
global using NewLife.Caching;