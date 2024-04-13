global using AutoFixture;
global using AutoMapper;
global using FluentAssertions;
global using FluentValidation;
global using Moq;
global using NetArchTest.Rules;
global using Newtonsoft.Json;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using System.Reflection;
global using System.Net;

global using ReverseAnalytics.Domain.Common;
global using ReverseAnalytics.Domain.Entities;
global using ReverseAnalytics.Domain.DTOs.ProductCategory;
global using ReverseAnalytics.Domain.Exceptions;
global using ReverseAnalytics.Domain.QueryParameters;
global using ReverseAnalytics.Domain.Interfaces.Services;
global using ReverseAnalytics.Infrastructure.Persistence;
global using ReverseAnalytics.Infrastructure.Repositories;
global using ReverseAnalytics.Services;
global using Reverse_Analytics.Api.Controllers;
global using ReverseAnalytics.Tests.Unit.Constants;