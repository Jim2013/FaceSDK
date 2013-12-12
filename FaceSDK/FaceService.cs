using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSDK
{
    public class FaceService : BaseService
    {
        #region Constructor
        public FaceService(string app_key, string app_secret)
            : base(app_key, app_secret)
        { }

        public FaceService()
            : base()
        { }

        #endregion

        #region 人脸检测与分析
        /// <summary>
        /// 检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性
        /// </summary>
        /// <param name="url">待检测图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是none或者由逗号分割的属性列表。默认为gender, age, race, smiling。目前支持的属性包括：gender, age, race, smiling, glass, pose</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不包含非法字符且不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns></returns>
        public DetectResponse Detection_Detect(string url, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {   
                {"url",url}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpGet<DetectResponse>("/detection/detect", dictionary);
        }

        /// <summary>
        /// 检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性
        /// </summary>
        /// <param name="bt">给定图片的二进制数据</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是none或者由逗号分割的属性列表。默认为gender, age, race, smiling。目前支持的属性包括：gender, age, race, smiling, glass, pose</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不包含非法字符且不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns></returns>
        public DetectResponse Detection_DetectWithByte(byte[] bt, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>();
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpPost<DetectResponse>("/detection/detect", dictionary, bt);
        }

        /// <summary>
        /// 异步检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性;立即返回一个session id，稍后可通过/info/get_session查询结果
        /// </summary>
        /// <param name="url">待检测图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是none或者由逗号分割的属性列表。默认为gender, age, race, smiling。目前支持的属性包括：gender, age, race, smiling, glass, pose</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不包含非法字符且不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns>session_id</returns>
        public AsyncResponse Detection_AsyncDetect(string url, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"url", url}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/detection/detect", dictionary);
        }

        /// <summary>
        /// 异步检测给定图片(Image)中的所有人脸(Face)的位置和相应的面部属性;立即返回一个session id，稍后可通过/info/get_session查询结果
        /// </summary>
        /// <param name="bt">给定图片的二进制数据</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="attribute">可以是none或者由逗号分割的属性列表。默认为gender, age, race, smiling。目前支持的属性包括：gender, age, race, smiling, glass, pose</param>
        /// <param name="tag">可以为图片中检测出的每一张Face指定一个不包含非法字符且不超过255字节的字符串作为tag，tag信息可以通过 /info/get_face 查询</param>
        /// <returns>session_id</returns>
        public AsyncResponse Detection_AsyncDetectWithByte(byte[] bt, string mode = "", string attribute = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>();
            if (mode != "") dictionary.Add("mode", mode);
            if (attribute != "") dictionary.Add("attribute", attribute);
            if (tag != "") dictionary.Add("tag", tag);
            return HttpPost<AsyncResponse>("/detection/detect", dictionary, bt);
        }

        /// <summary>
        /// 检测给定人脸(Face)相应的面部轮廓，五官等关键点的位置，包括25点和83点两种模式。
        /// </summary>
        /// <param name="face_id">待检测人脸的face_id</param>
        /// <param name="type">表示返回的关键点个数，目前支持83p或25p，默认为83p</param>
        /// <returns></returns>
        public LandmarkResponse Detection_Landmark(string face_id, string type = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id}
            };
            if (type != "") dictionary.Add("type", type);
            return HttpGet<LandmarkResponse>("/detection/landmark", dictionary);
        }
        #endregion

        #region 模型训练

        /// <summary>
        /// 针对verify功能对一个person进行训练。
        /// </summary>
        /// <param name="person_id">验证对象person_id</param>
        /// <returns>session_id</returns>
        public AsyncResponse Train_VerifyById(string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_id", person_id}
            };

            return HttpGet<AsyncResponse>("/train/verify", dictionary);
        }

        /// <summary>
        /// 针对verify功能对一个person进行训练。
        /// </summary>
        /// <param name="person_name">验证对象person_name</param>
        /// <returns>session_id</returns>
        public AsyncResponse Train_VerifyByName(string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"person_name", person_name}
            };

            return HttpGet<AsyncResponse>("/train/verify", dictionary);
        }

        /// <summary>
        /// 针对search功能对一个faceset进行训练。
        /// </summary>
        /// <param name="faceset_id">用于搜索的face组成的faceset_id</param>
        /// <returns>session_id</returns>
        public AsyncResponse Train_SearchById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id ", faceset_id }
            };

            return HttpGet<AsyncResponse>("/train/search", dictionary);
        }

        /// <summary>
        /// 针对search功能对一个faceset进行训练。
        /// </summary>
        /// <param name="faceset_name">用于搜索的face组成的faceset_name</param>
        /// <returns>session_id</returns>
        public AsyncResponse Train_SearchByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name}
            };

            return HttpGet<AsyncResponse>("/train/search", dictionary);
        }

        /// <summary>
        /// 针对identify功能对一个Group进行训练。
        /// </summary>
        /// <param name="group_id">识别候选人组成的group_id</param>
        /// <returns>session_id</returns>
        public AsyncResponse Train_IdentifyById(string group_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id ", group_id }
            };

            return HttpGet<AsyncResponse>("/train/identify", dictionary);
        }

        /// <summary>
        /// 针对identify功能对一个Group进行训练。
        /// </summary>
        /// <param name="group_name">识别候选人组成的group_name</param>
        /// <returns>session_id</returns>
        public AsyncResponse Train_IdentifyByName(string group_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };

            return HttpGet<AsyncResponse>("/train/identify", dictionary);
        }

        #endregion

        #region 人脸识别

        /// <summary>
        /// 计算两个Face的相似性以及五官相似度
        /// </summary>
        /// <param name="face_id1">第一个Face的face_id</param>
        /// <param name="face_id2">第二个Face的face_id</param>
        /// <returns></returns>
        public CompareResponse Recognition_Compare(string face_id1, string face_id2)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id1", face_id1},
                {"face_id2", face_id2}
            };

            return HttpGet<CompareResponse>("/recognition/compare", dictionary);
        }

        /// <summary>
        /// 异步计算两个Face的相似性以及五官相似度
        /// </summary>
        /// <param name="face_id1">第一个Face的face_id</param>
        /// <param name="face_id2">第二个Face的face_id</param>
        /// <returns>session_id</returns>
        public AsyncResponse Recognition_AsyncCompare(string face_id1, string face_id2)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id1", face_id1},
                {"face_id2", face_id2}
            };
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/compare", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Person，返回是否是同一个人的判断以及置信度。
        /// </summary>
        /// <param name="face_id">待verify的face_id</param>
        /// <param name="person_id">对应的person_id</param>
        /// <returns></returns>
        public VerifyResponse Recognition_VerifyById(string face_id, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id},
                {"person_id", person_id}
            };

            return HttpGet<VerifyResponse>("/recognition/verify", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Person，返回是否是同一个人的判断以及置信度。
        /// </summary>
        /// <param name="face_id">待verify的face_id</param>
        /// <param name="person_name">对应的person_name</param>
        /// <returns></returns>
        public VerifyResponse Recognition_VerifyByName(string face_id, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id},
                {"person_name", person_name}
            };

            return HttpGet<VerifyResponse>("/recognition/verify", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Person，异步返回是否是同一个人的判断以及置信度。
        /// </summary>
        /// <param name="face_id">待verify的face_id</param>
        /// <param name="person_id">对应的person_id</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncVerifyById(string face_id, string person_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id},
                {"person_id", person_id}
            };
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/verify", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Person，异步返回是否是同一个人的判断以及置信度。
        /// </summary>
        /// <param name="face_id">待verify的face_id</param>
        /// <param name="person_name">对应的person_name</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncVerifyByName(string face_id, string person_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id},
                {"person_name", person_name}
            };
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/verify", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Faceset，在该Faceset内搜索最相似的Face。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_id">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public SearchResponse Recognition_SearchById(string key_face_id, string faceset_id, int count = 3)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_id", faceset_id}
            };
            if (count > 0) dictionary.Add("count", count);
            return HttpGet<SearchResponse>("/recognition/search", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Faceset，在该Faceset内搜索最相似的Face。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_id">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public SearchResponse Recognition_SearchByName(string key_face_id, string faceset_name, int count = 3)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_name", faceset_name}
            };
            if (count > 0) dictionary.Add("count", count);
            return HttpGet<SearchResponse>("/recognition/search", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Faceset，异步在该Faceset内搜索最相似的Face。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_id">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncSearchById(string key_face_id, string faceset_id, int count = 3)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_id", faceset_id}
            };
            if (count > 0) dictionary.Add("count", count);
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/search", dictionary);
        }

        /// <summary>
        /// 给定一个Face和一个Faceset，异步在该Faceset内搜索最相似的Face。
        /// </summary>
        /// <param name="key_face_id">待搜索的Face的face_id</param>
        /// <param name="faceset_id">指定搜索范围为此Faceset</param>
        /// <param name="count">表示一共获取不超过count个搜索结果。默认count=3</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncSearchByName(string key_face_id, string faceset_name, int count = 3)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"key_face_id", key_face_id},
                {"faceset_name", faceset_name}
            };
            if (count > 0) dictionary.Add("count", count);
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/search", dictionary);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_id">识别候选人组成的group_id</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。仅当给出了url时，本选项有效。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResponse Recognition_IdentifyById(string group_id, string url = "", string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (url != "") dictionary.Add("url", url);
            if (mode != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpGet<IdentifyResponse>("/recognition/identify", dictionary);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_name">识别候选人组成的group_name</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。仅当给出了url时，本选项有效。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResponse Recognition_IdentifyByName(string group_name, string url = "", string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (url != "") dictionary.Add("url", url);
            if (mode != "" && url != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpGet<IdentifyResponse>("/recognition/identify", dictionary);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_id">识别候选人组成的group_id</param>
        /// <param name="bt">待识别图片的二进制数据</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResponse Recognition_IdentifyByIdWithByte(string group_id, byte[] bt, string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpPost<IdentifyResponse>("/recognition/identify", dictionary, bt);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_name">识别候选人组成的group_name</param>
        /// <param name="bt">待识别图片的二进制数据</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public IdentifyResponse Recognition_IdentifyByNameWithByte(string group_name, byte[] bt, string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            return HttpPost<IdentifyResponse>("/recognition/identify", dictionary, bt);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），异步在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_id">识别候选人组成的group_id</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。仅当给出了url时，本选项有效。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncIdentifyById(string group_id, string url = "", string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (url != "") dictionary.Add("url", url);
            if (mode != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/identify", dictionary);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），异步在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_name">识别候选人组成的group_name</param>
        /// <param name="url">待识别图片的URL</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。仅当给出了url时，本选项有效。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncIdentifyByName(string group_name, string url = "", string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (url != "") dictionary.Add("url", url);
            if (mode != "" && url != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            dictionary.Add("async", "true");
            return HttpGet<AsyncResponse>("/recognition/identify", dictionary);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），异步在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_id">识别候选人组成的group_id</param>
        /// <param name="bt">待识别图片的二进制数据</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncIdentifyByIdWithByte(string group_id, byte[] bt, string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_id", group_id}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            dictionary.Add("async", "true");
            return HttpPost<AsyncResponse>("/recognition/identify", dictionary, bt);
        }

        /// <summary>
        /// 对于一个待查询的Face列表（或者对于给定的Image中所有的Face），异步在一个Group中查询最相似的Person。
        /// </summary>
        /// <param name="group_name">识别候选人组成的group_name</param>
        /// <param name="bt">待识别图片的二进制数据</param>
        /// <param name="mode">检测模式可以是normal(默认) 或者 oneface 。在oneface模式中，检测器仅找出图片中最大的一张脸。</param>
        /// <param name="key_face_id">开发者也可以指定一个face_id的列表来表明对这些face进行识别。可以设置此参数key_face_id为一个逗号隔开的face_id列表。</param>
        /// <returns></returns>
        public AsyncResponse Recognition_AsyncIdentifyByNameWithByte(string group_name, byte[] bt, string mode = "", string key_face_id = "")
        {
            var dictionary = new Dictionary<object, object>
            {
                {"group_name", group_name}
            };
            if (mode != "") dictionary.Add("mode", mode);
            if (key_face_id != "") dictionary.Add("key_face_id", key_face_id);
            dictionary.Add("async", "true");
            return HttpPost<AsyncResponse>("/recognition/identify", dictionary, bt);
        }


        #endregion

        #region 人脸聚类与分组

        /// <summary>
        /// 给出一个Faceset，尝试将其分类，使得来自同一个人的Face被放在同一类中。
        /// </summary>
        /// <param name="faceset_id">相应Faceset的id</param>
        /// <returns></returns>
        public AsyncResponse Grouping_GroupingById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_id", faceset_id}
            };

            return HttpGet<AsyncResponse>("/grouping/grouping", dictionary);
        }

        /// <summary>
        /// 给出一个Faceset，尝试将其分类，使得来自同一个人的Face被放在同一类中。
        /// </summary>
        /// <param name="faceset_name">相应Faceset的name</param>
        /// <returns></returns>
        public AsyncResponse Grouping_GroupingByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"faceset_name", faceset_name}
            };

            return HttpGet<AsyncResponse>("/grouping/grouping", dictionary);
        }

        #endregion

        #region Person管理

        /// <summary>
        /// 创建一个Person,一个Person最多允许包含10000个Face。
        /// </summary>
        /// <param name="person_name">Person的Name信息，必须在App中全局唯一。Name不能包含非法字符，且长度不得超过255。Name也可以不指定，此时系统将产生一个随机的name。</param>
        /// <param name="face_id">一组用逗号分隔的face_id, 表示将这些Face加入到该Person中</param>
        /// <param name="tag">Person相关的tag，不需要全局唯一，不能包含非法字符，长度不能超过255。</param>
        /// <param name="group_id">一组用逗号分割的group id列表。如果该参数被指定，该Person被create之后就会被加入到这些组中。</param>
        /// <param name="group_name">一组用逗号分割的group name列表。如果该参数被指定，该Person被create之后就会被加入到这些组中。</param>
        /// <returns></returns>
        public CreatePersonResponse Person_Create(string person_name = "", string face_id = "", string tag = "", string group_id = "", string group_name = "")
        {
            var dictionary = new Dictionary<object, object>();

            if (person_name != "") dictionary.Add("person_name", person_name);
            if (face_id != "") dictionary.Add("face_id", face_id);
            if (tag != "") dictionary.Add("tag", tag);
            if (group_id != "") dictionary.Add("group_id", group_id);
            if (group_name != "") dictionary.Add("group_name", group_name);

            return HttpGet<CreatePersonResponse>("/person/create", dictionary);
        }

        /// <summary>
        /// 删除一组Person
        /// </summary>
        /// <param name="person_id">用逗号隔开的待删除的Person id列表</param>
        /// <returns></returns>
        public DeleteResponse Person_DeleteById(string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"person_id", person_id}
            };

            return HttpGet<DeleteResponse>("/person/delete", dictionary);
        }

        /// <summary>
        /// 删除一组Person
        /// </summary>
        /// <param name="person_name">用逗号隔开的待删除的Person name列表</param>
        /// <returns></returns>
        public DeleteResponse Person_DeleteByName(string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"person_name", person_name}
            };

            return HttpGet<DeleteResponse>("/person/delete", dictionary);
        }

        /// <summary>
        /// 将一组Face加入到一个Person中。注意，一个Face只能被加入到一个Person中。一个Person最多允许包含10000个Face。
        /// </summary>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Person中。</param>
        /// <param name="person_id">相应Person的id</param>
        /// <returns></returns>
        public AddResponse Person_AddFaceWithId(string face_id, string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"face_id", face_id},
                 {"person_id", person_id}
            };

            return HttpGet<AddResponse>("/person/add_face", dictionary);
        }

        /// <summary>
        /// 将一组Face加入到一个Person中。注意，一个Face只能被加入到一个Person中。一个Person最多允许包含10000个Face。
        /// </summary>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Person中。</param>
        /// <param name="person_name">相应Person的name</param>
        /// <returns></returns>
        public AddResponse Person_AddFaceWithName(string face_id, string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"face_id", face_id},
                 {"person_name", person_name}
            };

            return HttpGet<AddResponse>("/person/add_face", dictionary);
        }

        /// <summary>
        /// 删除Person中的一个或多个Face
        /// </summary>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该Person中删除。开发者也可以指定face_id=all, 表示删除该Person所有相关Face。</param>
        /// <param name="person_id">相应Person的id</param>
        /// <returns></returns>
        public RemoveResponse Person_RemoveFaceWithId(string face_id, string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"face_id", face_id},
                 {"person_id", person_id}
            };

            return HttpGet<RemoveResponse>("/person/remove_face", dictionary);
        }

        /// <summary>
        /// 删除Person中的一个或多个Face
        /// </summary>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该Person中删除。开发者也可以指定face_id=all, 表示删除该Person所有相关Face。</param>
        /// <param name="person_name">相应Person的name</param>
        /// <returns></returns>
        public RemoveResponse Person_RemoveFaceWithName(string face_id, string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"face_id", face_id},
                 {"person_name", person_name}
            };

            return HttpGet<RemoveResponse>("/person/remove_face", dictionary);
        }

        /// <summary>
        /// 设置Person的name和tag
        /// </summary>
        /// <param name="person_id">相应Person的id</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public Person Person_SetInfoById(string person_id, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { 
                 {"person_id", person_id}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<Person>("/person/set_info", dictionary);
        }

        /// <summary>
        /// 设置Person的name和tag
        /// </summary>
        /// <param name="person_name">相应Person的name</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public Person Person_SetInfoByName(string person_name, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { 
                 {"person_name", person_name}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<Person>("/person/set_info", dictionary);
        }

        /// <summary>
        /// 获取一个Person的信息, 包括name, id, tag, 相关的face, 以及groups等信息
        /// </summary>
        /// <param name="person_id">相应Person的id</param>
        /// <returns></returns>
        public GetPersonInfoResponse Person_GetInforById(string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"person_id", person_id}
            };

            return HttpGet<GetPersonInfoResponse>("/person/get_info", dictionary);
        }

        /// <summary>
        /// 获取一个Person的信息, 包括name, id, tag, 相关的face, 以及groups等信息
        /// </summary>
        /// <param name="person_name">相应Person的name</param>
        /// <returns></returns>
        public GetPersonInfoResponse Person_GetInforByName(string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"person_name", person_name}
            };

            return HttpGet<GetPersonInfoResponse>("/person/get_info", dictionary);
        }

        #endregion

        #region FaceSet管理
        /// <summary>
        /// 创建一个Faceset, 一个Faceset最多允许包含10000个Face。
        /// </summary>
        /// <param name="faceset_name">Faceset的Name信息，必须在App中全局唯一。Name不能包含非法字符，且长度不得超过255。Name也可以不指定，此时系统将产生一个随机的name。</param>
        /// <param name="face_id">一组用逗号分隔的face_id, 表示将这些Face加入到该Faceset中</param>
        /// <param name="tag">Faceset相关的tag，不需要全局唯一，不能包含非法字符，长度不能超过255。</param>
        /// <returns></returns>
        public CreatePersonResponse FaceSet_Create(string faceset_name = "", string face_id = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>();
            if (faceset_name != "") dictionary.Add("faceset_name", faceset_name);
            if (face_id != "") dictionary.Add("face_id", face_id);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<CreatePersonResponse>("/faceset/create", dictionary);
        }

        /// <summary>
        /// 删除一组Faceset
        /// </summary>
        /// <param name="faceset_id">用逗号隔开的待删除的faceset id列表</param>
        /// <returns></returns>
        public DeleteResponse FaceSet_DeleteById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_id", faceset_id}
            };

            return HttpGet<DeleteResponse>("/faceset/delete", dictionary);
        }

        /// <summary>
        /// 删除一组Faceset
        /// </summary>
        /// <param name="faceset_name">用逗号隔开的待删除的faceset name列表</param>
        /// <returns></returns>
        public DeleteResponse FaceSet_DeleteByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_name ", faceset_name }
            };

            return HttpGet<DeleteResponse>("/faceset/delete", dictionary);
        }

        /// <summary>
        /// 将一组Face加入到一个Faceset中。一个Faceset最多允许包含10000个Face。
        /// </summary>
        /// <param name="faceset_id">相应Faceset的id</param>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Faceset中。</param>
        /// <returns></returns>
        public AddResponse FaceSet_AddFaceWithId(string faceset_id, string face_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_id", faceset_id},
                 {"face_id", face_id}
            };

            return HttpGet<AddResponse>("/faceset/add_face", dictionary);
        }

        /// <summary>
        /// 将一组Face加入到一个Faceset中。一个Faceset最多允许包含10000个Face。
        /// </summary>
        /// <param name="faceset_name">相应Faceset的name</param>
        /// <param name="face_id">一组用逗号分隔的face_id,表示将这些Face加入到相应Faceset中。</param>
        /// <returns></returns>
        public AddResponse FaceSet_AddFaceWithName(string faceset_name, string face_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_name", faceset_name},
                 {"face_id", face_id}
            };

            return HttpGet<AddResponse>("/faceset/add_face", dictionary);
        }

        /// <summary>
        /// 删除Faceset中的一个或多个Face
        /// </summary>
        /// <param name="faceset_id">相应faceset的id</param>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该faceset中删除。开发者也可以指定face_id=all, 表示删除该faceset所有相关Face。</param>
        /// <returns></returns>
        public RemoveResponse FaceSet_RemoveFaceWithId(string faceset_id, string face_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_id", faceset_id},
                 {"face_id", face_id}
            };

            return HttpGet<RemoveResponse>("/faceset/remove_face", dictionary);
        }

        /// <summary>
        /// 删除Faceset中的一个或多个Face
        /// </summary>
        /// <param name="faceset_name">相应faceset的name</param>
        /// <param name="face_id">一组用逗号分隔的face_id列表，表示将这些face从该faceset中删除。开发者也可以指定face_id=all, 表示删除该faceset所有相关Face。</param>
        /// <returns></returns>
        public RemoveResponse FaceSet_RemoveFaceWithName(string faceset_name, string face_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_name ", faceset_name },
                 {"face_id", face_id}
            };

            return HttpGet<RemoveResponse>("/faceset/remove_face", dictionary);
        }

        /// <summary>
        /// 设置faceset的name和tag
        /// </summary>
        /// <param name="faceset_id">相应faceset的id</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public Person FaceSet_SetInfoById(string faceset_id, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_id", faceset_id}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<Person>("/faceset/set_info", dictionary);
        }

        /// <summary>
        /// 设置faceset的name和tag
        /// </summary>
        /// <param name="faceset_name">相应faceset的name</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public Person FaceSet_SetInfoByName(string faceset_name, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_name", faceset_name}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<Person>("/faceset/set_info", dictionary);
        }

        /// <summary>
        /// 获取一个faceset的信息, 包括name, id, tag, 以及相关的face等信息
        /// </summary>
        /// <param name="faceset_id ">相应faceset的id</param>
        /// <returns></returns>
        public GetPersonInfoResponse FaceSet_GetInforById(string faceset_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_id ", faceset_id }
            };

            return HttpGet<GetPersonInfoResponse>("/faceset/get_info", dictionary);
        }

        /// <summary>
        /// 获取一个faceset的信息, 包括name, id, tag, 以及相关的face等信息
        /// </summary>
        /// <param name="faceset_name">相应faceset的name</param>
        /// <returns></returns>
        public GetPersonInfoResponse FaceSet_GetInforByName(string faceset_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"faceset_name", faceset_name}
            };

            return HttpGet<GetPersonInfoResponse>("/faceset/get_info", dictionary);
        }

        #endregion

        #region Group管理
        /// <summary>
        /// 创建一个Group, 一个Group最多允许包含10000个Person。
        /// </summary>
        /// <param name="group_name">Group的Name信息，必须在App中全局唯一。Name不能包含非法字符，且长度不得超过255。Name也可以不指定，此时系统将产生一个随机的name。</param>
        /// <param name="person_id">一组用逗号分隔的person_id或person_name, 表示将这些Person加入到该Group中。注意，一个Person可以被加入到多个Group中。</param>
        /// <param name="tag">Group的tag，不需要全局唯一，不能包含非法字符，长度不能超过255。</param>
        /// <returns></returns>
        public CreatePersonResponse Group_Create(string group_name = "", string person_id = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object>();
            if (group_name != "") dictionary.Add("group_name", group_name);
            if (person_id != "") dictionary.Add("person_id", person_id);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<CreatePersonResponse>("/group/create", dictionary);
        }

        /// <summary>
        ///删除一组Group
        /// </summary>
        /// <param name="group_id">用逗号隔开的待删除的gropu id列表</param>
        /// <returns></returns>
        public DeleteResponse Group_DeleteById(string group_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id", group_id}
            };

            return HttpGet<DeleteResponse>("/group/delete", dictionary);
        }

        /// <summary>
        /// 删除一组Group
        /// </summary>
        /// <param name="group_name ">用逗号隔开的待删除的gropu id列表</param>
        /// <returns></returns>
        public DeleteResponse Group_DeleteByName(string group_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name  ", group_name  }
            };

            return HttpGet<DeleteResponse>("/group/delete", dictionary);
        }

        /// <summary>
        /// 将一组Person加入到一个Group中。一个Group最多允许包含10000个Person。
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_id">一组用逗号分隔的person_id，表示将这些Person加入到相应Group中。</param>
        /// <returns></returns>
        public AddResponse Group_AddPersonByGroupIdPersonId(string group_id, string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id", group_id},
                 {"person_id", person_id}
            };

            return HttpGet<AddResponse>("/group/add_person", dictionary);
        }

        /// <summary>
        /// 将一组Person加入到一个Group中。一个Group最多允许包含10000个Person。
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_name">一组用逗号分隔的person_name，表示将这些Person加入到相应Group中。</param>
        /// <returns></returns>
        public AddResponse Group_AddPersonByGroupIdPersonName(string group_id, string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id", group_id},
                 {"person_name", person_name}
            };

            return HttpGet<AddResponse>("/group/add_person", dictionary);
        }

        /// <summary>
        /// 将一组Person加入到一个Group中。一个Group最多允许包含10000个Person。
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_id">一组用逗号分隔的person_id或person_name，表示将这些Person加入到相应Group中。</param>
        /// <returns></returns>
        public AddResponse Group_AddPersonByGroupNamePersonId(string group_name, string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name", group_name},
                 {"person_id", person_id}
            };

            return HttpGet<AddResponse>("/group/add_person", dictionary);
        }

        /// <summary>
        /// 将一组Person加入到一个Group中。一个Group最多允许包含10000个Person。
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_name">一组用逗号分隔的person_name，表示将这些Person加入到相应Group中。</param>
        /// <returns></returns>
        public AddResponse Group_AddPersonByGroupNamePersonName(string group_name, string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name", group_name},
                 {"person_name", person_name}
            };

            return HttpGet<AddResponse>("/group/add_person", dictionary);
        }

        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_id">一组用逗号分割的person_id列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。</param>
        /// <returns></returns>
        public RemoveResponse Group_RemovePersonByGroupIdPersonId(string group_id, string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id", group_id},
                 {"person_id", person_id}
            };

            return HttpGet<RemoveResponse>("/group/remove_person", dictionary);
        }

        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="person_name">一组用逗号分割的person_name列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。</param>
        /// <returns></returns>
        public RemoveResponse Group_RemovePersonByGroupIdPersonName(string group_id, string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id", group_id},
                 {"person_name", person_name}
            };

            return HttpGet<RemoveResponse>("/group/remove_person", dictionary);
        }

        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_id">一组用逗号分割的person_id列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。</param>
        /// <returns></returns>
        public RemoveResponse Group_RemovePersonByGroupNamePersonId(string group_name, string person_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name", group_name},
                 {"person_id", person_id}
            };

            return HttpGet<RemoveResponse>("/group/remove_person", dictionary);
        }

        /// <summary>
        /// 从Group中删除一组Person
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="person_name">一组用逗号分割的person_name列表，表示将这些person从该Group中删除。开发者也可以指定person_id=all, 表示删掉该Group中所有Person。</param>
        /// <returns></returns>
        public RemoveResponse Group_RemovePersonByGroupNamePersonName(string group_name, string person_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name", group_name},
                 {"person_name", person_name}
            };

            return HttpGet<RemoveResponse>("/group/remove_person", dictionary);
        }

        /// <summary>
        /// 设置Group的name和tag
        /// </summary>
        /// <param name="group_id">相应Group的id</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public Group Group_SetInfoById(string group_id, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id ", group_id }
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<Group>("/group/set_info", dictionary);
        }

        /// <summary>
        /// 设置Group的name和tag
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <param name="name">新的name</param>
        /// <param name="tag">新的tag</param>
        /// <returns></returns>
        public Person Group_SetInfoByName(string group_name, string name = "", string tag = "")
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name", group_name}
            };
            if (name != "") dictionary.Add("name", name);
            if (tag != "") dictionary.Add("tag", tag);

            return HttpGet<Person>("/group/set_info", dictionary);
        }

        /// <summary>
        /// 获取一个Group的信息, 包括name, id, tag, 以及相关的face等信息
        /// </summary>
        /// <param name="group_id  ">相应Group的id</param>
        /// <returns></returns>
        public GetGroupInfoResponse Group_GetInforById(string group_id)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_id  ", group_id  }
            };

            return HttpGet<GetGroupInfoResponse>("/group/get_info", dictionary);
        }

        /// <summary>
        /// 获取一个Group的信息, 包括name, id, tag, 以及相关的face等信息
        /// </summary>
        /// <param name="group_name">相应Group的name</param>
        /// <returns></returns>
        public GetGroupInfoResponse Group_GetInforByName(string group_name)
        {
            var dictionary = new Dictionary<object, object> { 
                 {"group_name", group_name}
            };

            return HttpGet<GetGroupInfoResponse>("/group/get_info", dictionary);
        }

        #endregion

        #region 信息查询

        /// <summary>
        /// 获取一张image的信息, 包括其中包含的face等信息
        /// </summary>
        /// <param name="img_id">目标图片的img_id</param>
        /// <returns></returns>
        public GetImageResponse Info_GetImage(string img_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"img_id", img_id}
            };

            return HttpGet<GetImageResponse>("/info/get_image", dictionary);
        }

        /// <summary>
        /// 给定一组Face，返回相应的信息(包括源图片, 相关的person等等)。
        /// </summary>
        /// <param name="face_id">一组用逗号分割的face_id</param>
        /// <returns></returns>
        public GetFaceResponse Info_GetFace(string face_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"face_id", face_id}
            };

            return HttpGet<GetFaceResponse>("/info/get_face", dictionary);
        }

        /// <summary>
        /// 返回该App中的所有Person
        /// </summary>
        /// <returns></returns>
        public GetPersonListResponse Info_GetPersonList()
        {
            var dictionary = new Dictionary<object, object>();

            return HttpGet<GetPersonListResponse>("/info/get_person_list", dictionary);
        }

        /// <summary>
        /// 返回该App中的所有faceset
        /// </summary>
        /// <returns></returns>
        public GetFaceSetListResponse Info_GetFaceSetList()
        {
            var dictionary = new Dictionary<object, object>();

            return HttpGet<GetFaceSetListResponse>("/info/get_faceset_list", dictionary);
        }

        /// <summary>
        /// 返回这个App中的所有Group
        /// </summary>
        /// <returns></returns>
        public GetGroupListResponse Info_GetGroupList()
        {
            var dictionary = new Dictionary<object, object>();

            return HttpGet<GetGroupListResponse>("/info/get_group_list", dictionary);
        }

        /// <summary>
        /// 获取session相关状态和结果。可能的status：INQUEUE(队列中), SUCC(成功) 和FAILED(失败)。当status是SUCC时，返回结果中还包含session对应的结果。
        /// </summary>
        /// <param name="session_id">由/detection或/recognition中的API调用产生的session_id</param>
        /// <returns></returns>
        public Session Info_GetSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };

            return HttpGet<Session>("/info/get_session", dictionary);
        }

        /// <summary>
        /// 获取DetectionSession相关状态和结果。可能的status：INQUEUE(队列中), SUCC(成功) 和FAILED(失败)。当status是SUCC时，返回结果中还包含session对应的结果。
        /// </summary>
        /// <param name="session_id">由/detection或/recognition中的API调用产生的session_id</param>
        /// <returns></returns>
        public DetectionSessionResponse Info_GetDetectionSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };

            return HttpGet<DetectionSessionResponse>("/info/get_session", dictionary);
        }

        /// <summary>
        /// 获取CompareSession相关状态和结果。可能的status：INQUEUE(队列中), SUCC(成功) 和FAILED(失败)。当status是SUCC时，返回结果中还包含session对应的结果。
        /// </summary>
        /// <param name="session_id">由/detection或/recognition中的API调用产生的session_id</param>
        /// <returns></returns>
        public CompareSessionResponse Info_GetCompareSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };

            return HttpGet<CompareSessionResponse>("/info/get_session", dictionary);
        }

        /// <summary>
        /// 获取VerifySession相关状态和结果。可能的status：INQUEUE(队列中), SUCC(成功) 和FAILED(失败)。当status是SUCC时，返回结果中还包含session对应的结果。
        /// </summary>
        /// <param name="session_id">由/detection或/recognition中的API调用产生的session_id</param>
        /// <returns></returns>
        public VerifySessionResponse Info_GetVerifySession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };

            return HttpGet<VerifySessionResponse>("/info/get_session", dictionary);
        }

        /// <summary>
        /// 获取SearchSession相关状态和结果。可能的status：INQUEUE(队列中), SUCC(成功) 和FAILED(失败)。当status是SUCC时，返回结果中还包含session对应的结果。
        /// </summary>
        /// <param name="session_id">由/detection或/recognition中的API调用产生的session_id</param>
        /// <returns></returns>
        public SearchSessionResponse Info_GetSearchSession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };

            return HttpGet<SearchSessionResponse>("/info/get_session", dictionary);
        }

        /// <summary>
        /// 获取IdentifySession相关状态和结果。可能的status：INQUEUE(队列中), SUCC(成功) 和FAILED(失败)。当status是SUCC时，返回结果中还包含session对应的结果。
        /// </summary>
        /// <param name="session_id">由/detection或/recognition中的API调用产生的session_id</param>
        /// <returns></returns>
        public IdentifySessionResponse Info_GetIdentifySession(string session_id)
        {
            var dictionary = new Dictionary<object, object>
            {
                {"session_id", session_id}
            };

            return HttpGet<IdentifySessionResponse>("/info/get_session", dictionary);
        }

        /// <summary>
        /// 获取该App相关的信息
        /// </summary>
        /// <returns></returns>
        public GetAppResponse Info_GetApp()
        {
            var dictionary = new Dictionary<object, object>();

            return HttpGet<GetAppResponse>("/info/get_app", dictionary);
        }
        #endregion
    }
}
