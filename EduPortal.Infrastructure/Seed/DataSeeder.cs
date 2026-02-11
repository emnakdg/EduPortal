using EduPortal.Domain.Entities;
using EduPortal.Domain.Enums;
using EduPortal.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Infrastructure.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(
        EduPortalDbContext context,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(userManager);
        await SeedSubjectsAsync(context);
        await SeedTeachersAsync(context, userManager);
        await SeedClassesAsync(context);
        await SeedStudentsAsync(context, userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        string[] roles = { "Admin", "Teacher", "Student", "Parent" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
        }
    }

    private static async Task SeedAdminAsync(UserManager<AppUser> userManager)
    {
        var adminEmail = "admin@eduportal.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Sistem Yöneticisi",
                Role = UserRole.Admin,
                EmailConfirmed = true,
                IsActive = true
            };
            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    private static async Task SeedSubjectsAsync(EduPortalDbContext context)
    {
        if (await context.Subjects.AnyAsync()) return;

        var subjects = new List<Subject>
        {
            new Subject
            {
                Name = "Matematik", Code = "MAT", Color = "#3B82F6", GradeLevel = 10,
                Units = new List<Unit>
                {
                    new Unit { Name = "Fonksiyonlar", OrderIndex = 1, Topics = new List<Topic>
                    {
                        new Topic { Name = "Fonksiyon Kavramı ve Gösterimi", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Birebir ve Örten Fonksiyonlar", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Bileşke Fonksiyon", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Ters Fonksiyon", OrderIndex = 4, QuestionCount = 0 },
                        new Topic { Name = "Grafik Çizimi", OrderIndex = 5, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Polinomlar", OrderIndex = 2, Topics = new List<Topic>
                    {
                        new Topic { Name = "Polinom Kavramı", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Polinomlarda İşlemler", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Çarpanlara Ayırma", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Katsayılar ve Kökler", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Denklem ve Eşitsizlikler", OrderIndex = 3, Topics = new List<Topic>
                    {
                        new Topic { Name = "Birinci Dereceden Denklemler", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "İkinci Dereceden Denklemler", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Eşitsizlik Sistemleri", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Mutlak Değer Denklemleri", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Trigonometri", OrderIndex = 4, Topics = new List<Topic>
                    {
                        new Topic { Name = "Trigonometrik Oranlar", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Birim Çember", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Trigonometrik Denklemler", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Toplam-Fark Formülleri", OrderIndex = 4, QuestionCount = 0 }
                    }}
                }
            },
            new Subject
            {
                Name = "Fizik", Code = "FIZ", Color = "#8B5CF6", GradeLevel = 11,
                Units = new List<Unit>
                {
                    new Unit { Name = "Mekanik ve Dinamik", OrderIndex = 1, Topics = new List<Topic>
                    {
                        new Topic { Name = "Newton Hareket Yasaları", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Vektörler ve Kuvvet", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Sürtünme Kuvveti", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Dairesel Hareket", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "İş, Enerji ve Güç", OrderIndex = 2, Topics = new List<Topic>
                    {
                        new Topic { Name = "İş Kavramı", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Kinetik ve Potansiyel Enerji", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Enerjinin Korunumu", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Güç ve Verim", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Elektrik ve Manyetizma", OrderIndex = 3, Topics = new List<Topic>
                    {
                        new Topic { Name = "Elektrik Yükleri ve Coulomb Yasası", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Elektrik Alan ve Potansiyel", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Ohm Yasası ve Devreler", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Manyetik Alan", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Dalgalar ve Optik", OrderIndex = 4, Topics = new List<Topic>
                    {
                        new Topic { Name = "Dalga Mekaniği", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Ses Dalgaları", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Işığın Yansıması ve Kırılması", OrderIndex = 3, QuestionCount = 0 }
                    }}
                }
            },
            new Subject
            {
                Name = "Edebiyat", Code = "EDB", Color = "#F59E0B", GradeLevel = 12,
                Units = new List<Unit>
                {
                    new Unit { Name = "Şiir Bilgisi", OrderIndex = 1, Topics = new List<Topic>
                    {
                        new Topic { Name = "Şiir Türleri", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Edebi Sanatlar", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Ölçü ve Kafiye", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "İmge ve Sembol", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Roman ve Hikaye", OrderIndex = 2, Topics = new List<Topic>
                    {
                        new Topic { Name = "Roman Türleri", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Anlatım Teknikleri", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Karakter Çözümlemesi", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Olay Örgüsü ve Çatışma", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Dil Bilgisi", OrderIndex = 3, Topics = new List<Topic>
                    {
                        new Topic { Name = "Sözcük Türleri", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Cümle Yapısı ve Türleri", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Anlam Bilgisi", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Yazım ve Noktalama", OrderIndex = 4, QuestionCount = 0 }
                    }}
                }
            },
            new Subject
            {
                Name = "Kimya", Code = "KIM", Color = "#10B981", GradeLevel = 10,
                Units = new List<Unit>
                {
                    new Unit { Name = "Atom ve Periyodik Tablo", OrderIndex = 1, Topics = new List<Topic>
                    {
                        new Topic { Name = "Atom Modelleri", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Periyodik Tablo ve Özellikleri", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Elektron Dizilimi", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "İzotop ve İzobar", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Kimyasal Bağlar", OrderIndex = 2, Topics = new List<Topic>
                    {
                        new Topic { Name = "İyonik Bağ", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Kovalent Bağ", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Metalik Bağ", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Moleküller Arası Etkileşimler", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Kimyasal Tepkimeler", OrderIndex = 3, Topics = new List<Topic>
                    {
                        new Topic { Name = "Tepkime Denklemleri", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Mol Kavramı", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Stokiyometri", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Asit-Baz Tepkimeleri", OrderIndex = 4, QuestionCount = 0 }
                    }}
                }
            },
            new Subject
            {
                Name = "Biyoloji", Code = "BIY", Color = "#EC4899", GradeLevel = 11,
                Units = new List<Unit>
                {
                    new Unit { Name = "Hücre Biyolojisi", OrderIndex = 1, Topics = new List<Topic>
                    {
                        new Topic { Name = "Hücre Yapısı ve Organeller", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Hücre Zarı ve Madde Geçişi", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Mitoz Bölünme", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Mayoz Bölünme", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Genetik", OrderIndex = 2, Topics = new List<Topic>
                    {
                        new Topic { Name = "DNA ve RNA Yapısı", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Protein Sentezi", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Mendel Genetiği", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Kalıtım ve Mutasyon", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Ekosistem ve Canlılar", OrderIndex = 3, Topics = new List<Topic>
                    {
                        new Topic { Name = "Besin Zinciri ve Enerji Akışı", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Biyoçeşitlilik", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Çevre Sorunları", OrderIndex = 3, QuestionCount = 0 }
                    }}
                }
            },
            new Subject
            {
                Name = "Tarih", Code = "TAR", Color = "#F97316", GradeLevel = 10,
                Units = new List<Unit>
                {
                    new Unit { Name = "İlk Çağ Uygarlıkları", OrderIndex = 1, Topics = new List<Topic>
                    {
                        new Topic { Name = "Mezopotamya Uygarlıkları", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Mısır Uygarlığı", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Anadolu Uygarlıkları", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Çin ve Hint Uygarlıkları", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "İslam Tarihi", OrderIndex = 2, Topics = new List<Topic>
                    {
                        new Topic { Name = "Hz. Muhammed Dönemi", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Dört Halife Dönemi", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Emeviler ve Abbasiler", OrderIndex = 3, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Türk-İslam Devletleri", OrderIndex = 3, Topics = new List<Topic>
                    {
                        new Topic { Name = "Karahanlılar ve Gazneliler", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Büyük Selçuklu Devleti", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Anadolu Selçuklu Devleti", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Beylikler Dönemi", OrderIndex = 4, QuestionCount = 0 }
                    }},
                    new Unit { Name = "Osmanlı Devleti", OrderIndex = 4, Topics = new List<Topic>
                    {
                        new Topic { Name = "Kuruluş Dönemi", OrderIndex = 1, QuestionCount = 0 },
                        new Topic { Name = "Yükselme Dönemi", OrderIndex = 2, QuestionCount = 0 },
                        new Topic { Name = "Duraklama ve Gerileme", OrderIndex = 3, QuestionCount = 0 },
                        new Topic { Name = "Tanzimat ve Meşrutiyet", OrderIndex = 4, QuestionCount = 0 }
                    }}
                }
            }
        };

        context.Subjects.AddRange(subjects);
        await context.SaveChangesAsync();
    }

    private static async Task SeedTeachersAsync(EduPortalDbContext context, UserManager<AppUser> userManager)
    {
        if (await context.Teachers.AnyAsync()) return;

        var teachers = new[]
        {
            ("Ahmet Yılmaz",    "ahmet.yilmaz@eduportal.com",    "Matematik"),
            ("Fatma Demir",     "fatma.demir@eduportal.com",     "Fizik"),
            ("Mehmet Kaya",     "mehmet.kaya@eduportal.com",     "Edebiyat"),
            ("Ayşe Çelik",     "ayse.celik@eduportal.com",      "Kimya"),
            ("Mustafa Şahin",  "mustafa.sahin@eduportal.com",   "Biyoloji"),
            ("Zeynep Arslan",  "zeynep.arslan@eduportal.com",   "Tarih"),
            ("Ali Öztürk",     "ali.ozturk@eduportal.com",      "Matematik"),
            ("Elif Koç",       "elif.koc@eduportal.com",        "Fizik"),
            ("Hasan Aydın",    "hasan.aydin@eduportal.com",     "Edebiyat"),
            ("Merve Yıldız",   "merve.yildiz@eduportal.com",    "Kimya"),
        };

        foreach (var (fullName, email, branch) in teachers)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                Role = UserRole.Teacher,
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(user, "Teacher123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Teacher");
                context.Teachers.Add(new Teacher { UserId = user.Id, Branch = branch });
            }
        }
        await context.SaveChangesAsync();
    }

    private static async Task SeedClassesAsync(EduPortalDbContext context)
    {
        if (await context.Classes.AnyAsync()) return;

        var teachers = await context.Teachers.ToListAsync();

        var classes = new[]
        {
            ("9-A",  "2024-2025 Güz", "Derslik 101"),
            ("9-B",  "2024-2025 Güz", "Derslik 102"),
            ("10-A", "2024-2025 Güz", "Derslik 201"),
            ("10-B", "2024-2025 Güz", "Derslik 202"),
            ("11-A", "2024-2025 Güz", "Derslik 301"),
            ("11-B", "2024-2025 Güz", "Derslik 302"),
            ("12-A", "2024-2025 Güz", "Derslik 401"),
            ("12-B", "2024-2025 Güz", "Derslik 402"),
        };

        for (int i = 0; i < classes.Length; i++)
        {
            var (name, semester, room) = classes[i];
            context.Classes.Add(new Class
            {
                Name = name,
                Semester = semester,
                Room = room,
                TeacherId = teachers[i % teachers.Count].Id
            });
        }
        await context.SaveChangesAsync();
    }

    private static async Task SeedStudentsAsync(EduPortalDbContext context, UserManager<AppUser> userManager)
    {
        if (await context.Students.AnyAsync()) return;

        var classes = await context.Classes.ToListAsync();

        var studentNames = new[]
        {
            "Emre Acar", "Selin Bulut", "Kaan Çetin", "Deniz Dağ", "Ece Erdem",
            "Furkan Güneş", "Gamze Hakan", "Hüseyin İnan", "İrem Kılıç", "Serkan Koç",
            "Leyla Mert", "Murat Naz", "Nisa Oral", "Onur Polat", "Pınar Reis",
            "Ramazan Sarı", "Sude Taner", "Taha Uçar", "Ümit Vural", "Vildan Yalçın",
            "Berkay Zengin", "Ceren Akın", "Doğan Bayrak", "Esra Coşkun", "Ferhat Durmuş",
            "Gizem Eroğlu", "Halil Fidan", "Iraz Gökçe", "Jale Hamza", "Kerem İlhan",
            "Lale Kaplan", "Melih Laçin", "Nehir Meral", "Oğuz Nacar", "Pelin Olgun",
            "Rüya Parlak", "Sinan Korkut", "Tuğçe Sönmez", "Uğur Tekin", "Yasemin Uzun",
            "Arda Varol", "Buse Yaman", "Cem Zorlu", "Dilan Aksoy", "Erdem Bilge",
            "Firat Canan", "Gül Duran", "Harun Ekinci", "İlke Fatih", "Kübra Güzel",
            "Mete Han", "Nihan Işık", "Ozan Jandar", "Reyhan Kurt", "Selim Lale",
            "Tuba Mavi", "Utku Nesrin", "Yiğit Okan", "Zara Pınar", "Alperen Rüzgar"
        };

        for (int i = 0; i < studentNames.Length; i++)
        {
            var name = studentNames[i];
            var email = name.ToLower()
                .Replace(" ", ".")
                .Replace("ş", "s").Replace("ç", "c").Replace("ğ", "g")
                .Replace("ı", "i").Replace("ö", "o").Replace("ü", "u")
                .Replace("İ", "I")
                + "@ogrenci.eduportal.com";

            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = name,
                Role = UserRole.Student,
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(user, "Student123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Student");
                context.Students.Add(new Student
                {
                    UserId = user.Id,
                    StudentNumber = $"2024{(i + 1):D3}",
                    ClassId = classes[i % classes.Count].Id
                });
            }
        }
        await context.SaveChangesAsync();
    }
}