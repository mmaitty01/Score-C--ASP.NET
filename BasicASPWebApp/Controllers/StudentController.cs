using BasicASPWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using BasicASPWebApp.Data;

namespace BasicASPWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _db;

        public StudentController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            /*
            //การสร้าง Object จากโมเดล สามารถสร้างได้ 3 แบบดังนี้
            Student s1 = new Student();
            var s2 = new Student(); //รูปแบบนี้มีการนิยมใช้มากที่สุด
            Student s3 = new();

            s1.Id = 1;
            s1.Name = "มายเลิฟโค้ด";
            s1.Score = 100;

            s2.Id = 2;
            s2.Name = "มายเลิฟ C#";
            s2.Score = 40;

            s3.Id = 3;
            s3.Name = "มายเลิฟ ASP.NET";
            s3.Score = 50;

            List<Student> Allstudents = new List<Student>();
            Allstudents.Add(s1);
            Allstudents.Add(s2);
            Allstudents.Add(s3);
            */
            IEnumerable<Student> Allstudents = _db.Students;
            
            return View(Allstudents);

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //ถ้าไม่ระบุจะหมายถึงเป็น get
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student obj)
        {
            if (ModelState.IsValid)  //ใช้เพื่อตรวจสอบข้อมูลก่อนบันทึกเช่น คะแนนเกินหรือติดลบ ชื่อเป็นค่าว่าง
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id==0) 
            {
                return NotFound(); 
            }
            var obj = _db.Students.Find(id);
            if (obj == null) 
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost] //ถ้าไม่ระบุจะหมายถึงเป็น get
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student obj)
        {
            if (ModelState.IsValid)  //ใช้เพื่อตรวจสอบข้อมูลก่อนบันทึกเช่น คะแนนเกินหรือติดลบ ชื่อเป็นค่าว่าง
            {
                _db.Students.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //ค้นหาข้อมูล
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Students.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
