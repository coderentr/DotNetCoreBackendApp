using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CategoryModel
    {
        public Category category { get; set; }
        public IFormFile img { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryservice;
        public static IWebHostEnvironment _environment;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _categoryservice = categoryService;
            _environment = environment;
        }
        [HttpGet("getAllCategory")]
        public IActionResult GetCategoryList()
        {
            var result = _categoryservice.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getCategory")]
        public IActionResult GetById(int Id)
        {
            var result = _categoryservice.GetByCategory(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addCategory")]
        public IActionResult AddCategory([FromBody] Category model)
        {
            model.CreatedDate = DateTime.Now;
            var result = _categoryservice.Add(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("updateCategory")]
        public IActionResult UpdateCategory([FromBody]Category category)
        {
            category.UpdatedDate = DateTime.Now;
            var result = _categoryservice.Update(category);
            if (result.Success)
            {
                return Ok(result);
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
        [HttpDelete("delCatByCatId")]
        public IActionResult DeleteCatBtId(int id)
        {
            var cat = _categoryservice.GetByCategory(id).Data;
            if (cat != null)
            {
                var result = _categoryservice.Delete(cat);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPost("UpImg")]
        public bool UploadImg()
        {
            var httpRequest = HttpContext.Request;
            foreach (var file in httpRequest.Form.Files)
            {
                var postedFile = file;
                if (!Directory.Exists(_environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\" + postedFile.FileName))
                {
                    postedFile.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            return true;
        }
    }
}
