using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private IMasterService _masterservice;
        public static IWebHostEnvironment _environment;
        public MasterController(IMasterService masterService, IWebHostEnvironment environment)
        {
            _masterservice = masterService;
            _environment = environment;
        }
        [HttpGet("GetMasterItem")]
        [Authorize()]
        public IActionResult GetMasterItem()
        {
            var result = _masterservice.GetById(1);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UploadMasterPhoto")]
        [Authorize()]
        public bool UploadMasterPhoto()
        {
            var httpRequest = HttpContext.Request;
            foreach (var file in httpRequest.Form.Files)
            {
                var postedFile = file;
                if (!Directory.Exists(_environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\" + postedFile.FileName))
                {
                    postedFile.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            return true;
        }
        [HttpPost("deletemastersliderPhoto")]
        [Authorize()]
        public bool DeleteSilederPhoto(int id)
        {
            var sliderUrl = _masterservice.GetById(id).Data;
            if (sliderUrl != null)
            {
                var data = sliderUrl.SliderImgUrls.Split(',');
                try
                {
                    foreach (var item in data)
                    {
                        var file = _environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\" + item;
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

        [HttpPost("saveSlider")]
        [Authorize()]
        public IResult SaveSliderPhoto()
        {
            var response = true;
            var message = "İşlem Başarılı";
            var httpRequest = HttpContext.Request;
            var SliderImgPats = httpRequest.Form["SliderImgPats"].ToString();
            var SliderDescription = httpRequest.Form["SliderDescription"].ToString();
            var model = _masterservice.GetById(1);
            if (model != null)
            {
                if (model.Success)
                {
                    var data = model.Data;
                    if (httpRequest.Form.Files.Count > 0)
                    {
                        data.SliderImgUrls = SliderImgPats;
                    }
                    data.SliderDescription = SliderDescription;
                    var result = _masterservice.Update(data);
                    if (result.Success)
                    {
                        if (SaveSliderImg(httpRequest.Form.Files))
                        {
                            response = true;
                        }
                        else
                        {
                            response = false;
                            message = "Resim Eklenemedi";
                        }

                    }
                    else
                    {
                        message = result.Message;
                    }
                }
            }
            return new Result(response, message);
        }


        [HttpPost("saveDescriptionPhoto")]
        [Authorize()]
        public IResult SaveDescriptionPhoto()
        {
            var response = true;
            var message = "İşlem Başarılı";
            var httpRequest = HttpContext.Request;
            var DescriptionPhotoPats = httpRequest.Form["DescriptionPhotoPats"].ToString();
            var AboutPhotoPats = httpRequest.Form["AboutPhotoPats"].ToString();
            var Description = httpRequest.Form["Description"].ToString();
            var model = _masterservice.GetById(1);
            if (model != null)
            {
                if (model.Success)
                {
                    var data = model.Data;
                    data.Description = Description;
                    if (httpRequest.Form.Files.Count > 0)
                    {
                        data.DescriptionPhotoUrl = DescriptionPhotoPats;
                        data.AboutPhotoUrl = AboutPhotoPats;
                    }
                    var result = _masterservice.Update(data);
                    if (result.Success)
                    {
                        if (SaveSliderImg(httpRequest.Form.Files))
                        {
                            response = true;
                        }
                        else
                        {
                            response = false;
                            message = "Resim Eklenemedi";
                        }

                    }
                    else
                    {
                        message = result.Message;
                    }
                }
            }
            return new Result(response, message);
        }


        [HttpPost("SaveNiceWord")]
        [Authorize()]
        public IResult SaveNiceWord()
        {
            var response = true;
            var message = "İşlem Başarılı";
            var httpRequest = HttpContext.Request;
            var NiceWordImgPats = httpRequest.Form["NiceWordImgPats"].ToString();
            var NiceWordDescription = httpRequest.Form["NiceWordDescription"].ToString();
            var model = _masterservice.GetById(1);
            if (model != null)
            {
                if (model.Success)
                {
                    var data = model.Data;
                    data.NiceWordDescription = NiceWordDescription;
                    if (httpRequest.Form.Files.Count > 0)
                    {
                        data.NiceWordImgUrl = NiceWordImgPats;
                    }
                    var result = _masterservice.Update(data);
                    if (result.Success)
                    {
                        if (SaveSliderImg(httpRequest.Form.Files))
                        {
                            response = true;
                        }
                        else
                        {
                            response = false;
                            message = "Resim Eklenemedi";
                        }
                    }
                    else
                    {
                        message = result.Message;
                    }
                }
            }
            return new Result(response, message);
        }

        [HttpPost("SaveFooter")]
        [Authorize()]
        public IResult SaveFooter()
        {
            var response = true;
            var message = "İşlem Başarılı";
            var httpRequest = HttpContext.Request;
            var FooterPhotoPats = httpRequest.Form["FooterPhotoPats"].ToString();
            var model = _masterservice.GetById(1);
            if (model != null)
            {
                if (model.Success)
                {
                    var data = model.Data;
                    if (httpRequest.Form.Files.Count > 0)
                    {
                        data.FooterPhotoUrl = FooterPhotoPats;
                    }
                    var result = _masterservice.Update(data);
                    if (result.Success)
                    {
                        if (SaveSliderImg(httpRequest.Form.Files))
                        {
                            response = true;
                        }
                        else
                        {
                            response = false;
                            message = "Resim Eklenemedi";
                        }
                    }
                    else
                    {
                        message = result.Message;
                    }
                }
            }
            return new Result(response, message);
        }

        public bool SaveSliderImg(IFormFileCollection collection)
        {
            try
            {
                foreach (var file in collection)
                {
                    var postedFile = file;
                    if (!Directory.Exists(_environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Content\\Upload\\Img\\Master\\" + postedFile.FileName))
                    {
                        postedFile.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
