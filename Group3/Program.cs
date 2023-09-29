using Group3.Db;
using Group3.Reponsitory;
using Group3.Services;
using Group3.SmtpConfig;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// start Vinh
// Đọc cấu hình từ tệp appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
// end Vinh

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// start Vinh
// Đọc cấu hình SMTP từ appsettings.json và đăng ký nó trong DI container
var smtpConfig = configuration.GetSection("SmtpConfig").Get<SmtpConfig>();
builder.Services.AddSingleton(smtpConfig);
// end Vinh



//di
builder.Services.AddScoped<IAuthenService, AuthenServiceImp>();
builder.Services.AddScoped<IBrand, BrandService>();
builder.Services.AddScoped<ICat, CatService>();
builder.Services.AddScoped<ICreateAcountService, CreateAccountServiceImp>();
builder.Services.AddScoped<IBlogServices,BlogServiceImp>();
builder.Services.AddScoped<IContactDetail, ContactServicesImp>();
builder.Services.AddScoped<IGender, GenderService>();
builder.Services.AddScoped<IType, TypeService>();
builder.Services.AddScoped<IProd, ProdService>();

//google service
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

}).AddCookie(option =>
{

    option.Cookie.IsEssential = true;
})
    .AddGoogle(GoogleDefaults.AuthenticationScheme, opts =>
    {
        opts.ClientId = "164194168196-fjkt3ud3s7huq6odhut7h2klv1gsdmkv.apps.googleusercontent.com";
        opts.ClientSecret = "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";
        //opts.CallbackPath = "/signin-google";
        opts.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    
        opts.SaveTokens = true;
        opts.CorrelationCookie.SameSite = SameSiteMode.Lax;
        
    });

//service cookie SamSite
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.OnAppendCookie = cookieContext =>
        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
    options.OnDeleteCookie = cookieContext =>
        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);


});
void CheckSameSite(HttpContext httpContext, CookieOptions options)
{
    if (options.SameSite == SameSiteMode.None)
    {
        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
        if (MyUserAgentDetectionLib.DisallowsSameSiteNone(userAgent))
        {
            options.SameSite = SameSiteMode.Unspecified;
        }
    }
}
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Frontend/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Frontend}/{action=Index}/{id?}");


app.Run();

public class MyUserAgentDetectionLib
{
    public static bool DisallowsSameSiteNone(string userAgent)
    {
        // Check if a null or empty string has been passed in, since this
        // will cause further interrogation of the useragent to fail.
        if (String.IsNullOrWhiteSpace(userAgent))
            return false;

        // Cover all iOS based browsers here. This includes:
        // - Safari on iOS 12 for iPhone, iPod Touch, iPad
        // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
        // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
        // All of which are broken by SameSite=None, because they use the iOS networking
        // stack.
        if (userAgent.Contains("CPU iPhone OS 12") ||
            userAgent.Contains("iPad; CPU OS 12"))
        {
            return true;
        }

        // Cover Mac OS X based browsers that use the Mac OS networking stack. This includes:
        // - Safari on Mac OS X.
        // This does not include:
        // - Chrome on Mac OS X
        // Because they do not use the Mac OS networking stack.
        if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
            userAgent.Contains("Version/") && userAgent.Contains("Safari"))
        {
            return true;
        }

        // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
        // and none in this range require it.
        // Note: this covers some pre-Chromium Edge versions, 
        // but pre-Chromium Edge does not require SameSite=None.
        if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
        {
            return true;
        }

        return false;
    }
}
