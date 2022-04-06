using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

using System.Threading.Tasks;

using HelpDesk_AdminLte.Models;

using Microsoft.AspNet.Identity.Owin;
using System.Text.RegularExpressions;

namespace HelpDesk_AdminLte.Utilites
{
    public  class DBInitializer
    {
        public  void exec(ApplicationDbContext appDb)
        {
            try
            {
                if (appDb.Roles.Count() < 1)
                {
                    #region Create Departments
                    var itDepartm = new Departments()
                    {
                        Name = "Инфрастраструктуры",
                        Phone = "050343545545",
                        Location = "Запорожье",
                        Description = "Мы успешно поддерживаем более 80 компаний как отечественных, так и зарубежных" +
                                "Более сотни поддерживаемых серверов и тысячи проектов и сервисов" +
                                "Полный комплекс работ по обслуживанию инфраструктуры: планирование, внедрение, поддержка с обеспечение требуемого соглашения уровня облуживания" +
                                "Обращения регистрируются в системе сервис деска, все сбои четко фиксируются для предотвращение в дальнейшем." +
                                "В договоре на оказания услуг, прописываем SLA, который выполняем во время действия договора" +
                                "Подписываем NDA, гарантируя конфиденциальность данных клиента" +
                                "Передача на аутсорсинг ИТ инфраструктуры позволяет сэкономить 20 - 30 % затрат, при этом качество обслуживания гораздо выше."
                    };
                    var WEBdevelDepartm = new Departments()
                    {
                        Name = "Web разработки",
                        Phone = "+380612347658900",
                        Location = "Запорожье",
                        Description = "Наша компания уже многие годы работает в сфере веб-разработки, и опыт показывает, " +
                                  "что помимо технической компетенции важно понимание рыночной ниши и тонкостей построения бизнеса клиента." +
                                  "Мы стараемся держать фокус на узком круге индустрий, для того," +
                                  "чтобы более глубоко понимать потребности заказчиков и тем самым становиться лучшими в сфере создания веб-сайтов и приложений"
                    };
                    var oneCDevelDepartm = new Departments()
                    {
                        Name = "1C Поддержки",
                        Phone = "+3806123456745 ",
                        Location = "Запорожье",
                        Description = "Профессиональная поддержка 1С:Підприємство, обеспечивающая наиболее эффективную и стабильную работу системы автоматического учета, включает в себя как минимум три пункта:" +
                                        "Обновление 1С:Предприятие" +
                                        "Актуализация 1С: Предприятие." + "Необходимые доработки в 1С: Предприятие в связи с изменившимися требованиями заказчика"
                    };

                    appDb.Departments.Add(itDepartm);
                    appDb.Departments.Add(WEBdevelDepartm);
                    appDb.Departments.Add(oneCDevelDepartm);
                    #endregion

                    #region Create Companies
                    var CompJHL1 = new Companies() { Name = "JHL Company", Email = "office@jhl.com", Phone = "+38034433306", Location = "Дніпр" };
                    var CompOb2 = new Companies() { Name = "Оберег ЛТД", Email = "office@ober.com.ua", Phone = "+80234433306", Location = "ККиів" };
                    var CompTRS3 = new Companies() { Name = "ЧП Тирин", Email = "trs@gmail.com", Phone = "+380234433306", Location = "Запоріжжя" };
                    var CompRost4 = new Companies() { Name = "Менеджер РоСТ", Email = "manager@rost.com.ua", Phone = "+3801234433306", Location = "Киів" };

                    appDb.Companies.Add(CompJHL1);
                    appDb.Companies.Add(CompOb2);
                    appDb.Companies.Add(CompTRS3);
                    appDb.Companies.Add(CompRost4);
                    #endregion

                    appDb.SaveChanges();

                    string passwDefault = "Temp@123";

                    #region Create Supports
                  

                    var ivanovSupport = new Support()
                    {
                        Name = "Андрей Иванов",
                        Speciality = "Системный Администратор",
                        Email = "ivanov@contoso.com",
                        Phone = "+38050343545545",
                        Departments = itDepartm,
                        Image = @"/Files/PhotoUser/Ivanov.jpg",
                        
                     };
                    var user = createUserAppToUser_GetUser(appDb: appDb,roleName: "Support", email: ivanovSupport.Email, idClntSupp: ivanovSupport.SupportID.ToString());
                    ivanovSupport.UserID = user.Id;

                    var levitinSupport = new Support()
                    {
                        Name = "Артем Левитин",
                        Speciality = "С# программист",
                        Email = "artem.levitin@gmail.com",
                        Phone = "+38050343545545",
                        Departments = WEBdevelDepartm,
                        Image = @"/Files/PhotoUser/Levitin.png",
                        
                        
                    };
                    user = createUserAppToUser_GetUser(appDb: appDb, roleName: "Support", email: levitinSupport.Email, idClntSupp: levitinSupport.SupportID.ToString());
                    levitinSupport.UserID = user.Id;

                    var olegovSupport = new Support()
                    {
                        Name = "Олег Олегов",
                        Speciality = "FrontEnd программист",
                        Email = "olegov@contoso.com",
                        Phone = "+38050343545545",
                        Departments = WEBdevelDepartm,
                        Image = @"/Files/PhotoUser/Olegov.jpg",
                        
                       
                    };
                    user = createUserAppToUser_GetUser(appDb: appDb, password: passwDefault, roleName: "Support", email: olegovSupport.Email, idClntSupp: olegovSupport.SupportID.ToString());
                    olegovSupport.UserID = user.Id;

                    var serovaSupport = new Support()
                    {
                        Name = "Анна Серова",
                        Speciality = "1С программист",
                        Email = "Serova@contoso.com",
                        Phone = "+38050343547688",
                        Departments = oneCDevelDepartm,
                        Image = @"/Files/PhotoUser/Serova.jpg",
                        
                        
                    };
                    user = createUserAppToUser_GetUser(appDb: appDb,  roleName: "Support", email: serovaSupport.Email, idClntSupp: serovaSupport.SupportID.ToString());
                    serovaSupport.UserID = user.Id;

                    var artemovManager = new Support()
                    {
                        Name = "Игорь Артемов",
                        Speciality = "Административный Менеджер",
                        Email = "artemov@contoso.com",
                        Phone = "+3805034353454",
                        Departments = WEBdevelDepartm,
                        
                        Image = @"/Files/PhotoUser/Artemov.jpg"
                        
                    };
                    var artemovUser = createUserAppToUser_GetUser(appDb:appDb,  roleName:"Admin", email: artemovManager.Email,idClntSupp:artemovManager.SupportID.ToString());
                    artemovManager.UserID = artemovUser.Id;

                    appDb.Support.Add(ivanovSupport);
                    appDb.Support.Add(levitinSupport);
                    appDb.Support.Add(olegovSupport);
                    appDb.Support.Add(serovaSupport);
                    appDb.Support.Add(artemovManager);

                    appDb.SaveChanges();

                    #endregion

                    #region Create Clients

                    var cl1 = new Clients() { Name = "Ольга Данова", Email = "olga@jhl.com", Phone = "+3805033306",  Companies = CompJHL1, Image = @"/Files/PhotoUser/Danova.jpg",ClientID=Guid.NewGuid()};
                    var usrCln1 = createUserAppToUser_GetUser(appDb, email:cl1.Email, idClntSupp:cl1.ClientID.ToString());
                    cl1.UserID = usrCln1.Id;


                    var cl3 = new Clients() { Name = "Тірін Роман", Email = "tirin@gmail.com", Phone = "+04434433306", Companies = CompTRS3, Image = @"/Files/PhotoUser/Tirin.png", ClientID = Guid.NewGuid() };
                    var usrCln3 = createUserAppToUser_GetUser(appDb, email: cl3.Email, idClntSupp:cl3.ClientID.ToString());
                    cl3.UserID = usrCln3.Id;

                    var cl2 = new Clients() { Name = "Андрій Ситін", Email = "A.Sitin@ober.com.ua", Phone = "+38061233306", Companies = CompOb2, Image = @"/Files/PhotoUser/Sitin.jpg", ClientID = Guid.NewGuid() };
                    var usrCln2 = createUserAppToUser_GetUser(appDb, email: cl2.Email, idClntSupp: cl2.ClientID.ToString());
                    cl2.UserID = usrCln2.Id;

                    var cl4 = new Clients() { Name = "Ганна Рабіна", Email = "rabina@rost.com.ua", Phone = "+38061233306", Companies = CompRost4, Image = @"/Files/PhotoUser/Rabina.jpg", ClientID = Guid.NewGuid() };
                    var usrCln4 = createUserAppToUser_GetUser(appDb, email: cl4.Email, idClntSupp: cl4.ClientID.ToString());
                    cl4.UserID = usrCln4.Id;

                    appDb.Clients.Add(cl1);
                    appDb.Clients.Add(cl2);
                    appDb.Clients.Add(cl3);
                    appDb.Clients.Add(cl4);

                    appDb.SaveChanges();

                    #endregion

                    #region Create Priority and Status
                    var prMidl = new Priority() { Name = "Средний" };
                    var prLow = new Priority() { Name = "Низкий" };
                    var prHigh = new Priority() { Name = "Высокий" };
                    

                    appDb.Priority.Add(prMidl);
                    appDb.Priority.Add(prLow);
                    appDb.Priority.Add(prHigh);

                    var statusNew = new Status() { Name = "Новый" };
                    var statusProces = new Status() { Name = "В процессе" };
                    var statusCompl = new Status() { Name = "Завершен" };
                    var statusPause = new Status() { Name = "Приостановлен" };

                    appDb.Status.Add(statusNew);
                    appDb.Status.Add(statusProces);
                    appDb.Status.Add(statusCompl);
                    appDb.Status.Add(statusPause);

                    #endregion

                    appDb.SaveChanges();

                    #region Create Requests

                    if (appDb.Requests.Count() < 1)
                    {
                        appDb.Requests.Add(
                           new Requests()
                           {
                               NameRequest = "Исправить портал поддержки ",
                               CreatedByUserId = artemovManager.UserID,
                               Description = "Смотри замечания во вложении",
                               ClientID = cl4.ClientID,
                               SupportID = levitinSupport.SupportID,
                               PriorityID = prLow.PriorityID,
                               StatusID = statusProces.StatusID,
                               DateCreateRequest = new DateTime(2019, 05, 03, 11, 3, 1),
                               DateRequest = new DateTime(2019, 06, 20, 11, 3, 1),
                               File = "IssuePortal.txt",
                               FilePath= "IssuePortal.txt",
                               CommentResolve = "<p></p><p><i>Спасибо за заявку</i><br></p><ol><li><i>" +
                               "Замечания проанализировал</i></li><li><i>Внес исправления в код&nbsp;</i></li></ol><p></p><p></p><blockquote><p></p><p></p><ul><li><i>" +
                               "Изменил дизайн стартовой страницы</i></li><li><i>Убрал 'зависание' индикатора&nbsp; загрузки страницы pacer&nbsp;&nbsp;</i></li></ul><p></p><p></p></blockquote><p></p><p><b><i>" +
                               "Пожалуйста проверьте, есть ли еще замечания пишите</i></b></p><p></p><p><br></p>"

                           });

                        appDb.Requests.Add(
                                                new Requests()
                                                {
                                                    NameRequest = "Добавить печать ПДФ в контекстное меню ",
                                                    CreatedByUserId = cl4.UserID,
                                                    ClientID = cl4.ClientID,
                                                    SupportID = ivanovSupport.SupportID,
                                                    Description = @"​​На диске Z в Управление персоналом папке с названием Магазин Вознаграждений.С доступом для сотруднико службы персонала с редактирвванием, для всех остальных сотрудников только просмотр",
                                                    PriorityID = prMidl.PriorityID,
                                                    StatusID = statusCompl.StatusID,
                                                    CommentResolve = "Выгрузка восстановлена, файл сформировался корректно.",
                                                    DateCreateRequest = new DateTime(2019, 04, 12, 10, 32, 21),
                                                    DateRequest = new DateTime(2019, 04, 13, 12, 16, 10)
                                                });

                        //
                        appDb.Requests.Add(
                                               new Requests()
                                               {
                                                   NameRequest = "Ошибка при выгрузке прайса ",
                                                   CreatedByUserId = cl2.UserID,
                                                   ClientID = cl2.ClientID,
                                                   Description = @"​​Проблемы две: 1.Низкая скорость формирования файла(ранее файл выгружался за минут 10, сейчас формирование идет околочаса)2.Файл не создаеться, выбивает ошибку см скрин ",
                                                   SupportID = serovaSupport.SupportID,
                                                   PriorityID = prMidl.PriorityID,
                                                   StatusID = statusCompl.StatusID,
                                                   CommentResolve = "Выгрузка восстановлена, файл сформировался корректно.",
                                                   DateCreateRequest = new DateTime(2019, 05, 12, 10, 32, 21),
                                                   DateRequest = new DateTime(2019, 05, 17, 12, 16, 10)
                                               });


                        appDb.Requests.Add(
                                               new Requests()
                                               {
                                                   NameRequest = "Проблема с запуском 1 С ",
                                                   CreatedByUserId = cl2.UserID,
                                                   Description = @"​При запуске 1С появляется сообщение - Неверный формат хранилища данных(см.принтскрин) и далее программа не запускаетсяКак временное решение (до перезагрузки серверов) вручную создана строка подключения к рабочей базе. ",
                                                   SupportID = serovaSupport.SupportID,
                                                   ClientID = cl2.ClientID,
                                                   PriorityID = prMidl.PriorityID,
                                                   StatusID = statusNew.StatusID,
                                                   CommentResolve = "Пользователь недоступен в отпуске до 17 апреля",
                                                   DateCreateRequest = new DateTime(2019, 04, 12, 10, 32, 21),
                                                   DateRequest = new DateTime(2019, 04, 17, 12, 16, 10)
                                               });

                        appDb.Requests.Add(
                                               new Requests()
                                               {
                                                   NameRequest = "1C, корректировка регистров ",
                                                   CreatedByUserId = cl2.UserID,
                                                   Description = @"Корректировка записей регистров добавить РН ""Доходы""(для возможности корректировки) со всеми необходимыми полями/ аналитиками к заполнению.",
                                                   ClientID = cl2.ClientID,
                                                   SupportID = serovaSupport.SupportID,
                                                   PriorityID = prMidl.PriorityID,
                                                   StatusID = statusNew.StatusID,
                                                   DateCreateRequest = new DateTime(2019, 04, 29, 12, 32, 21),
                                                   DateRequest = new DateTime(2019, 04, 20, 12, 32, 21)
                                               });


                        appDb.Requests.Add(
                            new Requests()
                            {
                                NameRequest = "Доработка в КБ  ",
                                CreatedByUserId = cl2.UserID,
                                Description = @"​В 1С НС<u> Необходимо автоматизировать </u>заполнение платежных поручений исходящих комиссии банков, поскольку их может быть до 40 шт в каждом банке, что отнимает много времени на их проведение!&nbsp;<div><ul><li>&nbsp;Необходимые реквизиты платежа: 
 счет 92, статья затрат РКО(услуги банка); статья движ ден срв-Услуги банка(РКО)&nbsp;</li><li>&nbsp;Вид готовово ППисх прикрепляю, также прикрепляю фаил выписски из банка</li></ul></div>",
                                ClientID = cl2.ClientID,
                                SupportID = serovaSupport.SupportID,
                                PriorityID = prMidl.PriorityID,
                                StatusID = statusNew.StatusID,
                                DateCreateRequest = new DateTime(2019, 05, 09, 12, 32, 21),
                                DateRequest = new DateTime(2019, 06, 20, 12, 32, 21)
                            });

                        appDb.Requests.Add(
                            new Requests()
                            {
                                NameRequest = "Ошибки сайта продаж  ",
                                CreatedByUserId = cl2.UserID,
                                Description = @"Сайт компании <a target=""_blank"" rel=""nofollow"" href=""https://jhlcompany.com/"">https://jhlcompany.com/</a> подраздел продаж.<div><b>Ошибка в формировании прайса новых цен</b> </div> ",
                                ClientID = cl2.ClientID,
                                SupportID = ivanovSupport.SupportID,
                                PriorityID = prMidl.PriorityID,
                                StatusID = statusNew.StatusID,
                                DateCreateRequest = new DateTime(2019, 04, 24, 12, 32, 21),
                                DateRequest = new DateTime(2019, 07, 20, 12, 32, 21)
                            });

                        appDb.Requests.Add(
                            new Requests()
                            {
                                NameRequest = "Разработать отчет для отдела закупок  ",
                                CreatedByUserId = cl1.UserID,
                                Description = "Тех задание во вложении ",
                                ClientID = cl1.ClientID,
                                SupportID = serovaSupport.SupportID,
                                PriorityID = prLow.PriorityID,
                                StatusID = statusNew.StatusID,
                                DateCreateRequest = new DateTime(2019, 04, 28, 09, 12, 09),
                                DateRequest = new DateTime(2019, 06, 20, 09, 12, 09)
                            });

                        appDb.Requests.Add(
                           new Requests()
                           {
                               NameRequest = "Ошибка SharePoint портала предприятия",
                               CreatedByUserId = artemovManager.UserID,
                               Description = "Не загружается стартовая страница",
                               ClientID = cl1.ClientID,
                               SupportID = ivanovSupport.SupportID,
                               PriorityID = prHigh.PriorityID,
                               StatusID = statusNew.StatusID,
                               DateCreateRequest = new DateTime(2019, 04, 28, 09, 12, 09),
                               DateRequest = new DateTime(2019, 06, 20, 09, 12, 09)
                           });

                        appDb.Requests.Add(
                           new Requests()
                           {
                               NameRequest = "Непечатает принтер отчеты из 1С",
                               CreatedByUserId = cl2.UserID,
                               Description = "Ошибка  формированнии печати ",
                               ClientID = cl2.ClientID,
                               SupportID = serovaSupport.SupportID,
                               PriorityID = prHigh.PriorityID,
                               StatusID = statusNew.StatusID,
                               DateCreateRequest = new DateTime(2019, 04, 28, 11, 12, 20),
                               DateRequest = new DateTime(2019, 04, 30, 11, 12, 09)
                           });

                        appDb.Requests.Add(
                           new Requests()
                           {
                               NameRequest = "Незагружается компьютер финансового директора  ",
                               CreatedByUserId = cl3.UserID,
                               Description = "Синий экран ",
                               ClientID = cl3.ClientID,
                               SupportID = ivanovSupport.SupportID,
                               PriorityID = prMidl.PriorityID,
                               StatusID = statusNew.StatusID,
                               DateCreateRequest = new DateTime(2019, 05, 01, 11, 00, 41),
                               DateRequest = new DateTime(2019, 05, 03, 11, 00, 41)
                           });

                        appDb.Requests.Add(
                           new Requests()
                           {
                               NameRequest = "Разработать отчет для отдела закупок  ",
                               CreatedByUserId = artemovManager.UserID,
                               Description = "Тех задание во вложении ",
                               ClientID = cl3.ClientID,
                               SupportID = levitinSupport.SupportID,
                               PriorityID = prLow.PriorityID,
                               StatusID = statusProces.StatusID,
                               DateCreateRequest = new DateTime(2019, 06, 24, 10, 12, 21),
                               DateRequest = new DateTime(2019, 07, 20, 10, 12, 21)
                           });

                       

                        appDb.SaveChanges();
                    }
                    #endregion

                    #region Create Messages
                    if (appDb.Conversation.Count() < 1)
                    {
                        var convr1 = new Conversation() { OneUserID= artemovManager.UserID, TwoUserID= cl1.UserID };
                        var convr2 = new Conversation() { OneUserID = artemovManager.UserID, TwoUserID = levitinSupport.UserID };
                        var convr3 = new Conversation() { OneUserID = artemovManager.UserID, TwoUserID = cl3.UserID};

                        appDb.Conversation.Add(convr1);
                        appDb.Conversation.Add(convr2);
                        appDb.Conversation.Add(convr3);
                        appDb.SaveChanges();

                        appDb.Messages.Add(new Messages() { ConversationID = convr1.ConversationID, ToUserID= cl1.UserID, FromUserID = artemovManager.UserID, DateCreate = new DateTime(2019, 05, 12, 10, 20, 21), Text = "здравствуйте. Когда вам удобно будет обговорить условия поддержки" });
                        appDb.Messages.Add(new Messages() { ConversationID = convr1.ConversationID, ToUserID = artemovManager.UserID, FromUserID = cl1.UserID,  DateCreate = new DateTime(2019, 05, 12, 10, 32, 21), Text = "Пожалуйста позвоните мне в понедельник с 10 до 12 утра" });

                        appDb.Messages.Add(new Messages() { ConversationID = convr2.ConversationID, ToUserID = artemovManager.UserID, FromUserID = levitinSupport.UserID, DateCreate = new DateTime(2019, 06, 15, 18, 20, 23), Text = "День добрый помогите связаться с клиентом Ганна Рабіна ее телефон неотвечает" });

                        appDb.Messages.Add(new Messages() { ConversationID = convr3.ConversationID, ToUserID = artemovManager.UserID, FromUserID = cl3.UserID, DateCreate = new DateTime(2019, 06, 11, 8, 20, 11), Text = "День добрый пожалуйста ускорьте работу по нашей заявке" });
                        appDb.Messages.Add(new Messages() { ConversationID = convr3.ConversationID, ToUserID = cl3.UserID, FromUserID = artemovManager.UserID, DateCreate = new DateTime(2019, 06, 12, 12, 45, 09), Text = "Здравствуйте Пожалуйста позвоните уточните номер заявки" });
                    }
                    #endregion

                    appDb.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }

        }

       

     

        public ApplicationUser createUserAppToUser_GetUser(ApplicationDbContext appDb, string password = "Temp@123", string roleName = "Client", string email="", string idClntSupp = null)
        {
            try
            {
                
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDb));
                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                var roleStore = new RoleStore<IdentityRole>(appDb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);


                //Create Role if it does not exist
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = roleManager.Create(role);
                }

                var user = userManager.FindByName(email);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                // Add user user to Role Client if not already added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var result = userManager.AddToRole(user.Id, role.Name);
                }
                return user;
            }
            catch (Exception ex) { }
            {
                return null;
            }
        }



    }
}
