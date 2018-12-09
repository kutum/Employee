using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI.WebControls;
using EmployeeTable.Context;
using System.Threading.Tasks;
using EmployeeTable.Models;
using System.Data.Entity.Migrations;

namespace EmployeeTable.Contollers
{
    public class HomeController : Controller
    {
        /*
         * Контроллер домашней страницы (страница отсутствий)
         */


        AbsencesContext db = new AbsencesContext();

        //public ActionResult Index()
        //{

        //    var absences = db.Absences.Include(f => f.Employee);

        //    return View(absences.ToList());
        //}

        /// <summary>
        /// Обрабатывает GET запрос Index и формирует список отсутствий сотрудников, включая фильтры
        /// </summary>
        /// <param name="Fullname"></param>
        /// <param name="Name"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public ActionResult Index(string Fullname, string Name, string Reason)
        {
            //Основная выборка отсутствий с сотрудниками
            IQueryable<Absence> absences = db.Absences.Include(p => p.Employee);

            //В одном (или нескольких) if отфильтровываем записи
            if (!String.IsNullOrEmpty(Fullname) && Fullname != "Все")
            {
                absences = absences.Where(p => p.Employee.Fullname == Fullname).OrderBy(x => x.Date);
            }

            if (!String.IsNullOrEmpty(Name) && Name != "Все")
            {
                absences = absences.Where(p => p.Employee.Position.Name == Name).OrderBy(x => x.Date);
            }

            if (!String.IsNullOrEmpty(Reason) && Reason != "Все")
            {
                absences = absences.Where(p => p.Reason == Reason).OrderBy(x=>x.Date);
            }

            //Заполняем дропбоксы с фильтрами значениями из таблиц
            List<Employee> employees = db.Employees.ToList();
            employees.Insert(0, new Employee { Fullname = "Все", IdEmployee = 0 });

            List<Position> positions = db.Positions.ToList();
            positions.Insert(0, new Position {  Name = "Все", IdPosition = 0 });

            List<Absence> absenceslist = db.Absences.ToList();
            absenceslist.Insert(0, new Absence { IdAbsences = 0, idEmployee = 0, Reason = "Все" });

            //Создаём и заполняем модель данных
            AbsenceListViewModel alvm = new AbsenceListViewModel
            {
                Absences = absences.ToList(),
                Fullnames = new SelectList(employees, "Fullname", "Fullname"),
                Names = new SelectList(positions, "Name", "Name"),
                Reason = new SelectList(absenceslist, "Reason", "Reason")
            };
            return View(alvm);
        }

        /// <summary>
        /// Сотрудники включая должности
        /// </summary>
        /// <returns></returns>
        public ActionResult Employees()
        {
            IQueryable<Employee> employees = db.Employees.Include(p=>p.Position);
            return View(employees);
        }

        /// <summary>
        /// Создание записи об отсутствии (GET)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            //Заполняем дропбокс с ФИО сотрудников
            SelectList Fullnames = new SelectList(db.Employees, "idEmployee", "Fullname");
            ViewBag.Employees = Fullnames;

            return View();
        }
        /// <summary>
        /// (POST) Возвращение данных о сотруднике и добавление в БД, включая проверку данных
        /// </summary>
        /// <param name="absence"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([Bind(Include = "idEmployee,Date,Reason")]Absence absence)
        {
            //Если данные верны, то
            if (ModelState.IsValid)
            {
                //Добавляем запись об отсутствии
                db.Absences.Add(absence);
                //Применяем изменения к ЬД
                db.SaveChanges();
                //Возвращаемся на главную страницу (страницу отсутствий)
                return RedirectToAction("Index");
            }

            //Если не получилось, то снова заполняем дропбокс с сотрудниками 
            SelectList Fullnames = new SelectList(db.Employees, "idEmployee", "Fullname");
            ViewBag.Employees = Fullnames;

            //И идём обратно к заполнению данных для ввода и отображаем ошибки
            ViewBag.Message = "Некорректный ввод данных";
            return RedirectToAction("Employee");
        }

        /// <summary>
        /// Удаление записи об отсутствии, принимает id записи для поиска её в БЖ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //Ищем запись в БД
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {   
                //Не нашли :(
                return HttpNotFound();
            }
            //Иначе возвращаем её в представление
            return View(absence);
        }
        /// <summary>
        /// Удаление записи об обсутствии, после нажатия "Удалить". Принимает id записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Cнова ищем эту запись (вдруг она уже удалилась)
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {
                //Не нашли :(
                return HttpNotFound();
            }
            //Удаляем запись 
            db.Absences.Remove(absence);
            //Применяем изменения к БД
            db.SaveChanges();
            //Возвращаемся на основную страницу
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Добавлние нового сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Newemp()
        {
            return View();
        }
        /// <summary>
        /// Данные для добавления нового сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Newemp")]
        public ActionResult Newemp(Employee employee)
        {
            /*
             * Model.IsValid ругалась на отсутствующий Fullname поэтому здесь валидация происходит "вручную" (может можно как то лучше)
             * Если все поля (кроме ФИО) заполнены, то заполняем ФИО используя фамилию и первые буквы от имени и фамилии
             */
            if (employee.Surname != null && employee.Name != null &&
                employee.Lastname != null && employee.Position != null)
            {
                employee.Fullname = employee.Surname + " " +
                                    employee.Name.Substring(0, 1) + "." +
                                    employee.Lastname.Substring(0, 1) + ".";

                //Добавляем запись 
                db.Employees.Add(employee);
                //Применяем изменения в БД
                db.SaveChanges();
                //Показываем таблицу с добавленным сотрудником
                return RedirectToAction("Employees");
            }
            //Иначе возвращемся к странице ввода данных о новом сотруднике
            return View(employee);
        }

        /// <summary>
        /// Удаление сотрудника, принимает id сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delemp(int id)
        {
            //Сотрудник + должности, ищем сотрудника по id
            Employee employee = db.Employees.Include(p => p.Position).FirstOrDefault(x => x.IdEmployee == id);
            if (employee == null)
            {
                //Нету сотрудника :(
                return HttpNotFound();
            }
            //Возвращаем данные о сотруднике в представление
            return View(employee);
        }
        /// <summary>
        /// Данные для удаления сотрудника после ввода, принимает  id сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delemp")]
        public ActionResult DelempConfirmed(int id)
        {
            //Сотрудник + должности, ищем сотрудника по id
            Employee employee = db.Employees.Include(p=>p.Position).FirstOrDefault(x=>x.IdEmployee ==id);
            if (employee == null)
            {
                //Нету сотрудника :(
                return HttpNotFound();
            }
            //Удаляем
            db.Employees.Remove(employee);
            //Применяем изменения
            db.SaveChanges();
            //Отображаем страницу с сотрудниками
            return RedirectToAction("Employees");
        }

        /// <summary>
        /// Изменение данных об отсутствии, принимает id записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            //Заполняем дропбокс с ФИО сотрудников
            SelectList Fullnames = new SelectList(db.Employees, "idEmployee", "Fullname");
            ViewBag.Employees = Fullnames;
            if (id == null)
            {
                //Нет такой записи :(
                return HttpNotFound();
            }

            //Ищем запись по id
            Absence absence = db.Absences.Find(id);
            if (absence != null)
            {
                //Если запись нашли, возвращаем в представление
                return View(absence);
            }
            //Иначе говорим что не нашли
            return HttpNotFound();
        }

        /// <summary>
        /// Получение данных и удаление записи об отсутствии, принимает объект "Запись об отсутствии"
        /// </summary>
        /// <param name="absence"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modify(Absence absence)
        {
            //Обновляем запись
            db.Entry(absence).State = EntityState.Modified;
            //Сохраняем изменения в БД
            db.SaveChanges();
            //Возвращаемся на основную страницу
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Изменение данных о сотруднике, принимает id сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModifyEmp(int? id)
        {
            //Если не передали id сотрудника
            if (id == null)
            {
                //отобраем ошибку
                return HttpNotFound();
            }
            //Выборка сотрудников вместе с должностями с нужным id сотрудника
            Employee employee = db.Employees.Include(p=>p.Position).FirstOrDefault(x=>x.IdEmployee ==id);
            if (employee != null)
            {
                //если нашли такого, возвращаем на представление
                return View(employee);
            }
            //Иначе говорим что не нашлось такого
            return HttpNotFound();
        }

        /// <summary>
        /// Изменение данных о сотруднике, получаение данных после ввода, принимает объект "Сотрудник"
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyEmp(Employee employee)
        {
            //Проверяем введённые данные
            if (employee.Surname != null && employee.Name != null &&
                employee.Lastname != null && employee.Position != null)
            {
                //Если данные есть, формируем ФИО
                employee.Fullname = employee.Surname + " " +
                                employee.Name.Substring(0, 1) + "." +
                                employee.Lastname.Substring(0, 1) + ".";

                
                //Обновляем должность
                db.Entry(employee.Position).State = EntityState.Modified;
                //Обновляем данные о сотруднике
                db.Entry(employee).State = EntityState.Modified;
                //Сохраняем изменения в БД
                db.SaveChanges();
                //Возвращаемся к списку сотрудников
                return RedirectToAction("Employees");
            }
            else
                //иначе возвращаемся ко вводу данных
                return View(employee);
        }
    }
}