﻿using Microsoft.AspNetCore.Mvc;

namespace Reverse_Analytics.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController() : CommonControllerBase
{
    // GET: api/<HomeController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<HomeController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<HomeController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<HomeController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<HomeController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
