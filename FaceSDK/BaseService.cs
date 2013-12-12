using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSDK
{
    public class BaseService : ISerive
    {
        #region Fields
        /// <summary>
        /// App Key
        /// </summary>
        public string App_Key { get; set; }
        /// <summary>
        /// App Secret
        /// </summary>
        public string App_Secret { get; set; }
        /// <summary>
        /// Post/Get方法
        /// </summary>
        public IHttpMethod HttpMethod { get; private set; }
        /// <summary>
        /// baseUrl
        /// </summary>
        public readonly string baseUrl = "https://apicn.faceplusplus.com/v2{0}";
        #endregion

        #region Constructor
        public BaseService(string app_key, string app_secret)
        {
            this.App_Key = app_key;
            this.App_Secret = app_secret;
            this.HttpMethod = new HttpMethods();
        }

        public BaseService()
            : this(string.Empty,string.Empty)
        { }
        #endregion

        #region Methods

        #region Post
        public T HttpPost<T>(string partUrl, IDictionary<object, object> dictionary) where T : class
        {
            dictionary.Add("api_key", this.App_Key);
            dictionary.Add("api_secret", this.App_Secret);

            var url = this.baseUrl.ToFormat(partUrl);

            return HttpPost<T>(url, dictionary, null);
        }

        public T HttpPost<T>(string partUrl, IDictionary<object, object> dictionary, byte[] file) where T : class
        {
            dictionary.Add("api_key", this.App_Key);
            dictionary.Add("api_secret", this.App_Secret);

            var url = this.baseUrl.ToFormat(partUrl);
            var query = dictionary.ToQueryString();

            var json = string.Empty;
            if (file != null)
            {
                json = HttpMethod.HttpPost(url, dictionary, file);
            }
            else
            {
                json = HttpMethod.HttpPost(url, query);
            }

            return json.ToEntity<T>();
        }

        #endregion

        #region Get
        public T HttpGet<T>(string partUrl, IDictionary<object, object> dictionary) where T : class
        {
            dictionary.Add("api_key", this.App_Key);
            dictionary.Add("api_secret", this.App_Secret);

            var url = this.baseUrl.ToFormat(partUrl);

            var query = dictionary.ToQueryString();
            var json = HttpMethod.HttpGet(url + "?" + query);
            return json.ToEntity<T>("json");
        }

        public string HttpGet(string partUrl, IDictionary<object, object> dictionary)
        {
            dictionary.Add("api_key", this.App_Key);
            dictionary.Add("api_secret", this.App_Secret);

            var url = this.baseUrl.ToFormat(partUrl);
            var query = dictionary.ToQueryString();

            return this.HttpMethod.HttpGet(url + "?" + query);
        }

        #endregion

        #endregion
    }

    public interface ISerive
    {
        T HttpPost<T>(string partUrl, IDictionary<object, object> dictionary) where T : class;
        T HttpGet<T>(string partUrl, IDictionary<object, object> dictionary) where T : class;
    }
}
