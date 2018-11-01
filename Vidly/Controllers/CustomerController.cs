using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.membershipType).ToList();
            return View(customers);
        }

        //GET: Details
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.membershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel()
            {
                MembershipTypes = membershipType
            };
            return View("CustomerForm",viewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel()
                {
                   
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {   
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);

                customerInDB.Name = customer.Name;
                customerInDB.Birthday = customer.Birthday;
                customerInDB.membershipType = customer.membershipType;
                customerInDB.IsSubcribedToNewsletter = customer.IsSubcribedToNewsletter;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new NewCustomerViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
                
            };
            return View("CustomerForm", viewModel);
        }
        public ActionResult AddCustomer()
        {
            var membershipType = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel()
            {
                MembershipTypes = membershipType
            };
            return View("CustomerForm", viewModel);
        }
    }
}