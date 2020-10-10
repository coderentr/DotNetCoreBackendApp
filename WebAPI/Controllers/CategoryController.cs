﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
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
        [Authorize()]
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
        [Authorize()]
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
        [Authorize()]
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
        [Authorize()]
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
        [Authorize()]
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
        [Authorize()]
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
        [Authorize()]
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
        [HttpPost("deleteCategoryPhoto")]
        [Authorize()]
        public bool DeleteCategoryPhoto(int id)
        {
            var categoryUrl = _categoryservice.GetByCategory(id).Data;
            if (categoryUrl!=null)
            {
                var data = categoryUrl.ImgUrl.Split(',');
                try
                {
                    foreach (var item in data)
                    {
                        var file = _environment.WebRootPath + "\\Content\\Upload\\Img\\Category\\" + item;
                        System.IO.File.Delete(file);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
