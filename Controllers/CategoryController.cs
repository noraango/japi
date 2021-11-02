using System;
using System.Threading.Tasks;
using api.Models.DBModels;
using api.Repositories.Dependencies;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._CategoryRepository = categoryRepository;
        }

        // [HttpGet]
        // [Route("Add")]
        // public async Task<IActionResult> AddCategory()
        // {
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             Category cate = new Category();
        //             cate.Name = Request.Form["Name"];
        //             var cateID = await _CategoryRepository.CreateCategory(cate);
        //             if (cateID > 0)
        //             {
        //                 return Ok(cateID);
        //             }
        //             else
        //             {
        //                 return NotFound();
        //             }
        //         }
        //         catch (Exception)
        //         {
        //             return BadRequest();
        //         }
        //     }
        //     return BadRequest();
        // }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetCategoryList()
        {
            try
            {
                var cateList = await _CategoryRepository.GetListCategory();
                if (cateList == null)
                {
                    return NotFound();
                }
                return Ok(cateList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get/{ID}")]
        public async Task<IActionResult> GetCategoryByID(int ID)
        {
            try
            {
                var cate = await _CategoryRepository.GetCategoryByID(ID);
                if (cate == null)
                {
                    return NotFound();
                }
                return Ok(cate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("level/{level}")]
        public async Task<IActionResult> GetCategoriesByLevel(int level)
        {
            try
            {
                var cate = await _CategoryRepository.GetCategoriesByLevel(level);
                if (cate == null)
                {
                    return NotFound();
                }
                return Ok(cate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("subcategory/{id}")]
        public async Task<IActionResult> GetSubcategoriesById(int id)
        {
            try
            {
                var cate = await _CategoryRepository.GetSubcategoriesById(id);
                if (cate == null)
                {
                    return NotFound();
                }
                return Ok(cate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateCategory()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category cate = new Category();
                    cate.Id = Int32.Parse(Request.Form["Id"]);
                    cate.Name = Request.Form["Name"];
                    await _CategoryRepository.UpdateCategory(cate);
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // [HttpPost]
        // [Route("Delete")]
        // public async Task<IActionResult> DeleteCategory(int? Id)
        // {
        //     int result = 0;
        //     if (Id == null)
        //     {
        //         return BadRequest();
        //     }
        //     try
        //     {
        //         result = await _CategoryRepository.DeleteCategoryByID(Id);
        //         if (result == 0)
        //         {
        //             return NotFound();
        //         }
        //         return Ok();
        //     }
        //     catch (Exception)
        //     {
        //         return BadRequest();
        //     }
        // }
    }
}