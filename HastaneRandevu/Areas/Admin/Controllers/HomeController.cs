using HastaneRandevu.Areas.Admin.ViewModels;
using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTabAttribute("Home")]
    public class HomeController : Controller
    {
        // GET: Admin/Admin
        private const int PostPerPage = 10;

        public ActionResult Index(int page = 1)
        {
            int kullaniciSayisi = Database.Session.Query<User>().Count();
            var kullanicilar = Database.Session.Query<User>();
            var users = new List<UserInfo>();
            foreach (var user in kullanicilar)
            {
                users.Add(new UserInfo(user));
            }

            users
                .OrderBy(x => x.AdSoyad)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();

            return View(new UsersList() { Users = new PagedData<UserInfo>(users, kullaniciSayisi, page, PostPerPage)});
        }

        public ActionResult Klinikler(int page=1)
        {
            int klinikSayisi = Database.Session.Query<Klinik>().Count();
            var klinikler = Database.Session.Query<Klinik>();
            var kliniklist = new List<KlinikInfo>();
            foreach (var klinik in klinikler)
            {
                kliniklist.Add(new KlinikInfo(klinik));
            }

            kliniklist
                .OrderBy(x =>x.KlinikAdi)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();

            return View(new KlinikList() { Klinikler = new PagedData<KlinikInfo>(kliniklist, klinikSayisi, page, PostPerPage) });
        }

        public ActionResult Randevular(int page = 1)
        {
            int randevuSayisi = Database.Session.Query<Randevu>().Count();
            var randevular = Database.Session.Query<Randevu>();
            var randevulist = new List<RandevuInfo>();
            foreach (var randevu in randevular)
            {
                randevulist.Add(new RandevuInfo(randevu));
            }

            randevulist
                .OrderBy(x => x.Tarihi)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();

            return View(new RandevuList() { Randevular = new PagedData<RandevuInfo>(randevulist, randevuSayisi, page, PostPerPage) });
        }

        public ActionResult KlinikDuzenle(int id)
        {
            var klinik = Database.Session.Load<Klinik>(id);
            if (klinik == null)
                return HttpNotFound();
            return View("KlinikEkle", new KlinikNew()
            {
               Id = klinik.Id,
               isNew = false,
               KlinikAdi = klinik.KlinikAdi
            });
        }

        public ActionResult UserSil(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }

        public ActionResult KlinikSil(int id)
        {
            var klinik = Database.Session.Load<Klinik>(id);
            if (klinik == null)
                return HttpNotFound();

            Database.Session.Delete(klinik);
            Database.Session.Flush();
            return RedirectToAction("klinikler");
        }

        public ActionResult RandevuSil(int id)
        {
            var randevu = Database.Session.Load<Randevu>(id);
            if (randevu == null)
                return HttpNotFound();

            Database.Session.Delete(randevu);
            Database.Session.Flush();
            return RedirectToAction("randevular");
        }

        public ActionResult RandevuIptal(int id)
        {
            var randevu = Database.Session.Load<Randevu>(id);
            if (randevu == null)
                return HttpNotFound();
            randevu.Durum = "Admin İptal";
            Database.Session.Update(randevu);
            Database.Session.Flush();
            return RedirectToAction("randevular");
        }

        public List<SelectListItem> KlinikListesi(int id = 0)
        {
            var klinik = Database.Session.Query<Klinik>().ToList();

            List<SelectListItem> klinikler = new List<SelectListItem>();

            klinik.ForEach(x =>
            {
                
                klinikler.Add(new SelectListItem { Text = x.KlinikAdi, Value = x.Id.ToString(), Selected=(x.Id == id) });
            });

            return klinikler;
        }

        public ActionResult UserEkle()
        {
            return View(new UsersAdd()
            {
                Cinsiyetler = Database.Session.Query<Cinsiyet>().Select(
                    cinsiyet => new CinsiyetRadioBox()
                    {
                        Id = cinsiyet.Id,
                        Name = cinsiyet.Name
                    }).ToList(),
                Roles = Database.Session.Query<Role>().Select(
                    role => new RoleCheckBox()
                    {
                        Id = role.Id,
                        Name = role.Name
                    }).ToList(),
                Klinikler = KlinikListesi()
            }
            );
        }

        public ActionResult UserDuzenle(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            return View("Duzenle",new UsersAdd()
            {
                modifyPassword = false,
                Cinsiyetler = Database.Session.Query<Cinsiyet>().Select(
                    cinsiyet => new CinsiyetRadioBox()
                    {
                        Id = cinsiyet.Id,
                        Name = cinsiyet.Name,
                        IsChecked = user.Cinsiyet() == cinsiyet.Name
                    }).ToList(),
                Roles = Database.Session.Query<Role>().Select(
                    role => new RoleCheckBox()
                    {
                        Id = role.Id,
                        Name = role.Name,
                        IsChecked = user.Roles.Contains(role)
                    }).ToList(),
                Id = user.Id,
                Klinikler = KlinikListesi(id),
                KimlikNo = user.KimlikNo,
                Username = user.Username,
                DogumTarihi = user.DogumTarihi,
                Telefon = user.Telefon,
                Email = user.Email
            }
            );

        }

        [HttpPost]
        public ActionResult UserEkle(UsersAdd form)
        {
                if (Database.Session.Query<User>().Any(u => u.KimlikNo == form.KimlikNo))
                    ModelState.AddModelError("Kimlik No", "Kimlik No tek olmasi gerekir");
                if (!ModelState.IsValid)
                {
                    form.Cinsiyetler = Database.Session.Query<Cinsiyet>().Select(
                        cinsiyet => new CinsiyetRadioBox()
                        {
                            Id = cinsiyet.Id,
                            Name = cinsiyet.Name
                        }).ToList();
                    form.Roles = Database.Session.Query<Role>().Select(
                        role => new RoleCheckBox()
                        {
                            Id = role.Id,
                            Name = role.Name
                        }).ToList();
                    form.Klinikler = KlinikListesi();

                    return View(form);
                }
                var user = new User
                {
                    KimlikNo = form.KimlikNo,
                    Username = form.Username,
                    DogumTarihi = form.DogumTarihi,
                    Email = form.Email,
                    Telefon = form.Telefon
                };

                user.SetPassword(form.Password);
                SyncProperty(form.Roles, user.Roles);

                int refId = 0;
                SyncProperty(form.Cinsiyetler, ref refId);
                user.CinsiyetRefId = refId;
                Database.Session.Save(user);
                user = Database.Session.Query<User>().First(x => x.KimlikNo == form.KimlikNo);
                if (user.Roles.First().Name == "Admin")
                {
                    var doktor = new Administrator()
                    {
                        //Doktor detaylari girilecek
                    };
                }
            Database.Session.Flush();
            return RedirectToRoute("index");
        }

        [HttpPost]
        public ActionResult Duzenle(UsersAdd form)
        {
            var user = Database.Session.Query<User>().First(x => x.KimlikNo == form.KimlikNo);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (form.modifyPassword) //We preferred this to using Required then, adding and hiding extra fields in our view
            {
                if (form.Password == null)
                    ModelState.AddModelError("Password", "Sifre bos olamaz");
            }
            else
            {
                if (form.Email == null)
                    ModelState.AddModelError("Email", "Email gereklidir");
                if (form.Telefon == null)
                    ModelState.AddModelError("Telefon", "Telefon numara gereklidir");
            }

            if (!ModelState.IsValid)
                return RedirectToAction("UserDuzenle", new { id = form.Id });

            
            if (form.modifyPassword)
            {
                user.SetPassword(form.Password);
            }
            else
            {
                user.KimlikNo = form.KimlikNo;
                user.Username = form.Username;
                user.Email = form.Email;
                user.Telefon = form.Telefon;
               
                if (form.DogumTarihi != null)
                    user.DogumTarihi = form.DogumTarihi;
                if (form.Password != null)
                    user.SetPassword(form.Password);
                SyncProperty(form.Roles, user.Roles);

                int refId = 0;
                SyncProperty(form.Cinsiyetler, ref refId);
                user.CinsiyetRefId = refId;
            }

            Database.Session.Update(user);
            Database.Session.Flush();
            if (user.Roles.First().Name == "Admin" && form.modifyPassword == false)
            {
                var admin = new Administrator()
                {
                    AdminId = user.Id,
                    HastaneId = Auth.User.AdminDetay.HastaneId
                };
                if (Database.Session.Query<Administrator>().Any(x => x.AdminId == admin.AdminId))
                {
                    var ad = Database.Session.Query<Administrator>().First(x => x.AdminId == admin.AdminId);
                    ad.AdminId = admin.AdminId;
                    ad.HastaneId = admin.HastaneId;
                    Database.Session.Update(ad);
                }
                else
                {
                    Database.Session.Save(admin);
                }
            }
            Database.Session.Flush();
            return RedirectToAction("index", "Home");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            return View("Duzenle", new UsersAdd
            {
                Id = user.Id,
                KimlikNo = user.KimlikNo,
                modifyPassword = true
            });
        }

        public ActionResult KlinikEkle()
        {
            return View(new KlinikNew() { isNew = true});
        }

        [HttpPost]
        public ActionResult KlinikEkle(KlinikNew form)
        {
            if (form.Id == 0)
                if (Database.Session.Query<Klinik>().Any(u => u.KlinikAdi == form.KlinikAdi))
                     ModelState.AddModelError("Klinik", "Klinik zaten var");
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            Klinik klinik;
            if (form.Id == 0)
            {
                klinik = new Klinik()
                {
                    KlinikAdi = form.KlinikAdi
                };
            }
            else
            {
                klinik = Database.Session.Load<Klinik>(form.Id);
                klinik.KlinikAdi = form.KlinikAdi;
            }
            Database.Session.SaveOrUpdate(klinik);
            Database.Session.Flush();

            return RedirectToAction("klinikler");
        }

        private void SyncProperty(IList<RoleCheckBox> checkBoxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();


            foreach (var role in Database.Session.Query<Role>())
            {
                var checkBox = checkBoxes.Single(c => c.Id == role.Id);
                checkBox.Name = role.Name;

                if (checkBox.IsChecked)
                    selectedRoles.Add(role);
            }


            foreach (var toAdd in selectedRoles.Where(p => !roles.Contains(p)))
            {
                roles.Add(toAdd);
            }

            foreach (var toRemove in roles.Where(p => !selectedRoles.Contains(p)).ToList())
            {
                roles.Remove(toRemove);
            }


        }

        private void SyncProperty(IList<CinsiyetRadioBox> radioButtons, ref int refId)
        {
            foreach (var cinsiyet in Database.Session.Query<Cinsiyet>())
            {
                var radioButton = radioButtons.Single(c => c.Id == cinsiyet.Id);
                radioButton.Name = cinsiyet.Name;

                if (radioButton.IsChecked)
                {
                    refId = cinsiyet.Id;
                }
            }
        }
    }
}