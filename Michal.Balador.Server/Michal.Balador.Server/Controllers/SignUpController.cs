using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Web.Http;
using lior.api.Models;
using lior.AppStart.api;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Server.Dal;
using Michal.Balador.Server.Infrastructures.WebHookExstension;
using Michal.Balador.Server.Models;
using Microsoft.AspNet.WebHooks;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [Authorize]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SignUpController : ApiController
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SignUpController));
        [ImportMany(typeof(IFactrorySendMessages))]
        IEnumerable<Lazy<IFactrorySendMessages>> _senderRules;


        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post(RegisterBindingModel model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                   var userManager = new ApplicationUserManager(context);

                    ApplicationUser applicationUser = new ApplicationUser
                    {
                        Discriminator = Guid.NewGuid().ToString("N"),
                        PhoneNumber = model.Tel,
                        Email = model.Email,
                        EmailConfirmed = true,
                        PasswordHash = userManager.PasswordHasher.HashPassword(model.Password),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        TwoFactorEnabled=true,
                         PhoneNumberConfirmed=true,
                         Id=Guid.NewGuid().ToString(),
                        UserName = model.Tel
                    };
                    var creatr=await userManager.CreateAsync(applicationUser);
                   if(creatr.Succeeded)
                    {
                        response.IsError = false;
                        response.Message = ""; 
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message =String.Join(",", creatr.Errors);
                    }
                }
              
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            var responseToClient = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseBase>(response,
                        new JsonMediaTypeFormatter(),
                         new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return responseToClient;
        }
        [HttpGet]
        [Route("api/getMessagers")]
        public async Task<HttpResponseMessage> GetMessagers()
        {
            List<FormSignThirdPartyToken> authentications = new List<FormSignThirdPartyToken>();
            try
            {
                Michal.Balador.Contracts.DataModel.AuthenticationManager authenticationManager = null;
                MockRepository mockData = new MockRepository();
                foreach (var senderRule in _senderRules)
                {
                    authenticationManager = null;
                    var factory = senderRule.Value;

                    // var sender = await factory.GetInstance(new RegisterSender { Id = User.Identity.Name });

                    authenticationManager = await factory.GetAuthenticationManager(new RegisterSender { Id = User.Identity.Name });
                    var configuration = await authenticationManager.Register(new SignUpSender { Id = User.Identity.Name });

                    authentications.Add(new FormSignThirdPartyToken
                    {
                        Id = configuration.Id.GetHashCode().ToString(),
                        Fields = configuration.ExtraFields,
                        Message = configuration.TextLandPageTemplate,
                        Name = authenticationManager.AuthenticationName,
                        Title = authenticationManager.AuthenticationTitle,
                        IsAlreadyRegister = configuration.IsAlreadyRegister,
                        TwoFactorAuthentication = configuration.TwoFactorAuthentication
                    });
                }
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<FormSignThirdPartyToken[]>(authentications.ToArray(),
                             new JsonMediaTypeFormatter(),
                              new MediaTypeWithQualityHeaderValue("application/json"))
                };
                return response;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new ObjectContent<FormSignThirdPartyToken[]>(authentications.ToArray(),
                             new JsonMediaTypeFormatter(),
                              new MediaTypeWithQualityHeaderValue("application/json"))
                };
                return response;
            }

        }
        [HttpPost]
        [Route("api/signIn")]
        public async Task<HttpResponseMessage> SignIn(HttpRequestMessage request)
        {
            ResponseBase responseResult = new ResponseBase();
            try
            {
                NameValueCollection formData = await request.Content.ReadAsFormDataAsync();
                var id = formData["formType"];
                foreach (var senderRule in _senderRules)
                {
                    //var sender = await senderRule.Value.GetInstance(new RegisterSender {  Id = User.Identity.Name });
                    // if (!sender.IsError && sender.Result != null && sender.Result.ServiceName.GetHashCode().ToString() == id)
                    //  {
                    //var authenticationManager = senderRule.Value.AuthenticationManager;
                    var factory = senderRule.Value;
                    var authenticationManager = await factory.GetAuthenticationManager(new RegisterSender { Id = User.Identity.Name });

                    responseResult = await authenticationManager.SignIn(new SignUpSender { Id = User.Identity.Name }, formData);
                    break;
                    // }
                }
            }
            catch (Exception eee)
            {
                responseResult.IsError = true;
                responseResult.Message = eee.Message;
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseBase>(responseResult,
                        new JsonMediaTypeFormatter(),
                         new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }

        [HttpPost]
        [Route("api/unRegister")]
        public async Task<HttpResponseMessage> UnRegister(HttpRequestMessage request)
        {
            ResponseBase responseResult = new ResponseBase();
            try
            {
                NameValueCollection formData = await request.Content.ReadAsFormDataAsync();
                var id = formData["formType"];

                foreach (var senderRule in _senderRules)
                {
                    // var sender = await senderRule.Value.GetInstance(new RegisterSender { Id = User.Identity.Name });
                    // if (!sender.IsError && sender.Result != null && sender.Result.ServiceName.GetHashCode().ToString() == id)
                    // {
                    var factory = senderRule.Value;
                    var authenticationManager = await factory.GetAuthenticationManager(new RegisterSender { Id = User.Identity.Name });

                    //var authenticationManager = senderRule.Value.AuthenticationManager;
                    responseResult = await authenticationManager.UnRegister(new SignUpSender { Id = User.Identity.Name });
                    break;
                    //  }
                }
            }
            catch (Exception eee)
            {
                responseResult.IsError = true;
                responseResult.Message = eee.Message;
                //throw;
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseBase>(responseResult,
                        new JsonMediaTypeFormatter(),
                         new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }


        [HttpPost]
        [Route("api/SetToken")]
        public async Task<HttpResponseMessage> SetToken(HttpRequestMessage request)
        {
            ResponseBase responseResult = new ResponseBase();
            try
            {
                NameValueCollection formData = await request.Content.ReadAsFormDataAsync();
                var id = formData["formType"];
                var token = formData["token"];
                foreach (var senderRule in _senderRules)
                {
                    //var sender = await senderRule.Value.GetInstance(new RegisterSender { Id = User.Identity.Name });
                    //  if (!sender.IsError && sender.Result.ServiceName.GetHashCode().ToString() == id)
                    // {
                    var factory = senderRule.Value;
                    var authenticationManager = await factory.GetAuthenticationManager(new RegisterSender { Id = User.Identity.Name });

                    //   var authenticationManager = senderRule.Value.AuthenticationManager;
                    responseResult = await authenticationManager.SetObservableToken(new SignUpSender { Id = User.Identity.Name }, new BToken { Token = token });
                    break;
                    // }
                }
            }
            catch (Exception eee)
            {
                responseResult.IsError = true;
                responseResult.Message = eee.Message;
                //throw;
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseBase>(responseResult,
                        new JsonMediaTypeFormatter(),
                         new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }
    }
}