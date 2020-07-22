using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryservice;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryservice = categoryService;       
        }
        [HttpGet("getAllCategory")]
        public IActionResult GetCategoryList()
        {
           
                var result = _categoryservice.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getById")]
        public IActionResult GetById(int Id)
        {

            var result = _categoryservice.GetByCategory(Id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("addCategory")]
        public IActionResult AddCategory(Category category)
        {

            var result = _categoryservice.Add(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("updateCategory")]
        public IActionResult UpdateCategory(Category category)
        {

            var result = _categoryservice.Update(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("deleteCategory")]
        public IActionResult DeleteCategory(Category category)
        {

            var result = _categoryservice.Delete(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
