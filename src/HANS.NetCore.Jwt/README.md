#### HANS.NetCore.Jwt说明及其使用

##### 说明
- JWT，即JsonWebToken,本类库是基础`Asp.Net Core`封装好的Jwt验证方式
- 类库有两种验证方式：1、中间件方式，2、授权验证方式
- 优点：  
    1. 自由配置token秘钥及其失效分钟数
    2. 可自由定义验证函数
    3. 授权验证方式可根据自定义的策略，进行不同的验证
- 建议：两种验证方式，任选其一，目前还没整合一起，或是强制性一起使用，会先进行中间件的验证
- 缺陷：目前验证失败是直接抛出异常，并没有设定api类型返回

#### 使用  
1. `appsettings.json`的配置  
    - `appsettings.json` 需要配置以下节点，其中`ExpiresMinute`为失效时间，默认30分钟  
        ` "JsonWebToken": {
                    "SecurityKey": "HmacSha256的秘钥",
                    "ExpiresMinute":30
        } `

2. 中间件的使用 
    - 注册服务
        ` public void ConfigureServices(IServiceCollection services) {
         services.AddJwt(Configuration);}
        `
    - 使用中间件  
       `   public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseJwtCustomerAuthorize(option =>
            {
                //设置不会被验证的url，可以使用链式调用一直添加
                option.SetAnonymousPaths(new System.Collections.Generic.List<string>()
                {
                 //   "/",
                    "/Home/Privacy",
                    "/Home/CreateJsonToken"
                });
                // 自定义验证函数，playLoad为带过来的参数字典，setting为失效时间与秘钥
                option.SetValidateFunc((playLoad, sertting) =>
                {
                    return true;
                });
            });
        } `

3. 授权验证的使用  
    - 注册服务与直接注册授
    ` public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwt(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthorization(option =>
            {
                #region 自定义验证策略 可以一直自定义策略
                option.AddPolicy("common", policy => policy.Requirements.Add(new CommonAuthorize().
                    SetValidateFunc((playLoad, sertting) =>
                    {
                        //每个策略自定义验证函数，playLoad为带过来的参数字典，setting为失效时间与秘钥
                        return true;
                    })));
                #endregion 自定义验证策略
            }).AddAuthentication(option =>
            {
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        } `
        
    - 使用授权验证中间件
    `
     public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
        }
    `
    - Controller/Action应用
    `
        //使用JwtAuthorizeAttribute，定义策略名字
        [JwtAuthorizeAttribute(Policy = "common")]
        public IActionResult Index()
        {
            return View();
        }
    `
4. 生成token  
    ` public class HomeController : Controller
     {
        private readonly IJsonWebTokenBuilder jsonWebTokenBuilder;
        public HomeController(IJsonWebTokenBuilder jsonWebTokenBuilder)
        {
            this.jsonWebTokenBuilder = jsonWebTokenBuilder;
        }
        [HttpPost]
        public IActionResult CreateJsonToken(Dictionary<string, string> payLoad)
        {
            return Content(jsonWebTokenBuilder.CreateJsonWebToken(payLoad));
        }
    } `
