using AdminDashBoard.Helper;
using AdminDashBoard.Models;
using AutoMapper;
using Booking.Core.Models;
using Booking.Core.Repo.contract;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class EventsController : Controller
    {
        private readonly IGenericRepository<Events> _repository;
        private readonly IMapper _mapper;

        public EventsController(IGenericRepository<Events> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            var Events = await _repository.GetAllAsync();

            var mappedEvents = _mapper.Map<IReadOnlyList<Events>, IReadOnlyList<EventViewModel>>(Events);

            return View(mappedEvents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new EventViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newEvent = new Events
                {
                    Name = model.Name,
                    Description = model.Description,
                    Category = model.Category,
                    Price = model.Price,
                    Date = model.Date,
                    Venue = model.Venue
                };

                if (model.Image != null && model.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/events");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    newEvent.ImageName = uniqueFileName;
                }

                await _repository.AddAsync(newEvent);
                var count = await _repository.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Event Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }

            // لو في مشكلة نرجع نفس الفيو مع الموديل عشان نعرض الأخطاء
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventEntity = await _repository.GetAsync(id);
            if (eventEntity == null)
                return NotFound();

            var viewModel = _mapper.Map<EventViewModel>(eventEntity);
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EventViewModel Even)
        {
            if (ModelState.IsValid)
            {
                var eventInDb = await _repository.GetAsync(Even.Id);
                if (eventInDb == null)
                    return NotFound();

                // لو المستخدم رفع صورة جديدة
                if (Even.Image != null && Even.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/events");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Even.Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Even.Image.CopyToAsync(stream);
                    }

                    // حذف الصورة القديمة لو عايزة
                    if (!string.IsNullOrEmpty(eventInDb.ImageName))
                    {
                        var oldImagePath = Path.Combine(uploadsFolder, eventInDb.ImageName);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    Even.ImageName = uniqueFileName;
                }
                else
                {
                    // احتفظ باسم الصورة القديمة
                    Even.ImageName = eventInDb.ImageName;
                }

                // تحديث البيانات
                _mapper.Map(Even, eventInDb); // حدث الكائن الحالي

                await _repository.UpdateAsync(eventInDb);
                var result = await _repository.CompleteAsync();
                if (result > 0)
                {
                    TempData["Message"] = "Event Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(Even);
        }
        // عرض صفحة تأكيد الحذف
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Event = await _repository.GetAsync(id);

            if (Event == null)
                return NotFound();

            var mappedEvents = _mapper.Map<Events, EventViewModel>(Event);

            return View(mappedEvents);
        }

        // تنفيذ الحذف بعد التأكيد
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventInDb = await _repository.GetAsync(id);
            if (eventInDb == null)
                return NotFound();

            // حذف الصورة المرتبطة بالحدث إن وجدت
            if (!string.IsNullOrEmpty(eventInDb.ImageName))
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var imagePath = Path.Combine(uploadsFolder, eventInDb.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _repository.DeleteAsync(eventInDb);
            var result = await _repository.CompleteAsync();

            if (result > 0)
            {
                TempData["Message"] = "Event deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
