using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSDK
{
    public class DetectResponse : Image
    {
        public string session_id { get; set; }

        public int img_width { get; set; }

        public int img_height { get; set; }

        public IList<DetectFace> face { get; set; }

    }

    public class AsyncResponse
    {
        public string session_id { get; set; }
    }

    public class LandmarkResponse
    {
        public string session_id { get; set; }

        public IList<LandmarkResult> result { get; set; }
    }

    public class SearchResponse
    {
        public string session_id { get; set; }

        public IList<SearchCandidate> candidate { get; set; }
    }

    public class CompareResponse
    {
        public ComponentSimilarity componentSimilarity { get; set; }

        public string session_id { get; set; }

        public double similarity { get; set; }
    }

    public class VerifyResponse
    {
        public double confidence { get; set; }

        public bool is_same_person { get; set; }

        public string session_id { get; set; }
    }

    public class IdentifyResponse
    {
        public IList<IdentifyFace> face;

        public string session_id;
    }

    public class CreatePersonResponse : Person
    {
        public int added_face { get; set; }

        public int added_group { get; set; }

    }

    public class DeleteResponse
    {
        public int deleted { get; set; }

        public bool success { get; set; }
    }

    public class AddResponse
    {
        public int added { get; set; }

        public bool success { get; set; }
    }

    public class RemoveResponse
    {
        public int removed { get; set; }

        public bool success { get; set; }
    }

    public class GetPersonInfoResponse : Person
    {
        public IList<Face> face { get; set; }

        public IList<Group> group { get; set; }
    }

    public class CreateFaceSetResponse : FaceSet
    {
        public int added_face { get; set; }
    }

    public class GetFaceSetInfoResponse : FaceSet
    {
        public IList<Face> face { get; set; }
    }

    public class CreateGroupResponse : Group
    {
        public int added_person { get; set; }
    }

    public class GetGroupInfoResponse : Group
    {
        public IList<Person> person { get; set; }
    }

    public class GetImageResponse : Image
    {
        public IList<FaceWithPosition> face { get; set; }
    }

    public class GetFaceResponse
    {
        public IList<SessionResult> face_info { get; set; }
    }

    public class GetPersonListResponse
    {
        public IList<Person> person { get; set; }
    }

    public class GetFaceSetListResponse
    {
        public IList<FaceSet> faceset { get; set; }
    }

    public class GetGroupListResponse
    {
        public IList<Group> faceset { get; set; }
    }

    public class GetSessionResponse
    {
        public string create_time { get; set; }

        public string finish_time { get; set; }

        public string session_id { get; set; }

        public string status { get; set; }

        public SessionResult result { get; set; }
    }

    public class GetAppResponse
    {
        public string name { get; set; }

        public string description { get; set; }
    }

    public class SessionResult : Image
    {
        public int img_width { get; set; }

        public int img_height { get; set; }

        public IList<DetectFace> face { get; set; }
    }

    public class FaceInfo : Face
    {
        public Attribute attribute { get; set; }

        public IList<FaceSet> faceset { get; set; }

        public IList<Person> persopn { get; set; }

        public Position position { get; set; }

        public string img_id { get; set; }

        public string tag { get; set; }
    }

    public class LandmarkResult
    {
        public string face_id { get; set; }

        public Landmark landmark { get; set; }
    }

    public class Landmark 
    {
        public Point contour_chin { get; set; }

        public Point contour_left1 { get; set; }

        public Point contour_left2 { get; set; }

        public Point contour_left3 { get; set; }

        public Point contour_left4 { get; set; }

        public Point contour_left5 { get; set; }

        public Point contour_left6 { get; set; }

        public Point contour_left7 { get; set; }

        public Point contour_left8 { get; set; }

        public Point contour_left9 { get; set; }

        public Point contour_right1 { get; set; }

        public Point contour_right2 { get; set; }

        public Point contour_right3 { get; set; }

        public Point contour_right4 { get; set; }

        public Point contour_right5 { get; set; }

        public Point contour_right6 { get; set; }

        public Point contour_right7 { get; set; }

        public Point contour_right8 { get; set; }

        public Point contour_right9 { get; set; }

        public Point left_eye_bottom { get; set; }

        public Point left_eye_center { get; set; }

        public Point left_eye_left_corner { get; set; }

        public Point left_eye_lower_left_quarter { get; set; }

        public Point left_eye_lower_right_quarter { get; set; }

        public Point left_eye_pupil { get; set; }

        public Point left_eye_right_corner { get; set; }

        public Point left_eye_top { get; set; }

        public Point left_eye_upper_left_quarter { get; set; }

        public Point left_eye_upper_right_quarter { get; set; }

        public Point left_eyebrow_left_corner { get; set; }

        public Point left_eyebrow_lower_left_quarter { get; set; }

        public Point left_eyebrow_lower_middle { get; set; }

        public Point left_eyebrow_lower_right_quarter { get; set; }

        public Point left_eyebrow_right_corner { get; set; }

        public Point left_eyebrow_upper_left_quarter { get; set; }

        public Point left_eyebrow_upper_middle { get; set; }

        public Point left_eyebrow_upper_right_quarter { get; set; }

        public Point mouth_left_corner { get; set; }

        public Point mouth_lower_lip_bottom { get; set; }

        public Point mouth_lower_lip_left_contour1 { get; set; }

        public Point mouth_lower_lip_left_contour2 { get; set; }

        public Point mouth_lower_lip_left_contour3 { get; set; }

        public Point mouth_lower_lip_right_contour1 { get; set; }

        public Point mouth_lower_lip_right_contour2 { get; set; }

        public Point mouth_lower_lip_right_contour3 { get; set; }

        public Point mouth_lower_lip_top { get; set; }

        public Point mouth_right_corner { get; set; }

        public Point mouth_upper_lip_bottom { get; set; }

        public Point mouth_upper_lip_left_contour1 { get; set; }

        public Point mouth_upper_lip_left_contour2 { get; set; }

        public Point mouth_upper_lip_left_contour3 { get; set; }

        public Point mouth_upper_lip_right_contour1 { get; set; }

        public Point mouth_upper_lip_right_contour2 { get; set; }

        public Point mouth_upper_lip_right_contour3 { get; set; }

        public Point mouth_upper_lip_top { get; set; }

        public Point nose_contour_left1 { get; set; }

        public Point nose_contour_left2 { get; set; }

        public Point nose_contour_left3 { get; set; }

        public Point nose_contour_lower_middle { get; set; }

        public Point nose_contour_right1 { get; set; }

        public Point nose_contour_right2 { get; set; }

        public Point nose_contour_right3 { get; set; }

        public Point nose_left { get; set; }

        public Point nose_right { get; set; }

        public Point nose_tip { get; set; }

        public Point right_eye_bottom { get; set; }

        public Point right_eye_center { get; set; }

        public Point right_eye_left_corner { get; set; }

        public Point right_eye_lower_left_quarter { get; set; }

        public Point right_eye_lower_right_quarter { get; set; }

        public Point right_eye_pupil { get; set; }

        public Point right_eye_right_corner { get; set; }

        public Point right_eye_top { get; set; }

        public Point right_eye_upper_left_quarter { get; set; }

        public Point right_eye_upper_right_quarter { get; set; }

        public Point right_eyebrow_left_corner { get; set; }

        public Point right_eyebrow_lower_left_quarter { get; set; }

        public Point right_eyebrow_lower_middle { get; set; }

        public Point right_eyebrow_lower_right_quarter { get; set; }

        public Point right_eyebrow_right_corner { get; set; }

        public Point right_eyebrow_upper_left_quarter { get; set; }

        public Point right_eyebrow_upper_middle { get; set; }

        public Point right_eyebrow_upper_right_quarter { get; set; }
 
    }

    public class DetectFace : FaceWithPosition
    {
        public Attribute attribute { get; set; }

    }

    public class IdentifyFace : FaceWithPosition
    {
        public IList<IdentifyCandidate> candidate { get; set; }

    }

    public class Point
    {
        public double x { get; set; }

        public double y { get; set; }
    }

    public class Gender
    {
        public double confidence { get; set; }

        /// <summary>
        /// 值为Male/Female
        /// </summary>
        public string value { get; set; }
    }

    public class Age
    {
        public int range { get; set; }

        public int value { get; set; }
    }

    public class Race
    {
        public double confidence { get; set; }

        public string value { get; set; }
    }

    public class Smiling
    {
        public double value { get; set; }
    }

    public class Glass
    {
        public double confidence { get;set; }

        public string value { get; set; }
    }

    public class Pose
    {
        public double pitch_angle { get; set; }

        public double roll_angle { get; set; }

        public double yaw_angle { get; set; }

    }

    public class Attribute
    {
        public Age age { get; set; }

        public Gender gender { get; set; }

        public Glass glass { get; set; }

        public Pose pose { get; set; }

        public Race race { get; set; }

        public Smiling smiling { get; set; }
    }

    public class Position
    {
        public Point center { get; set; }

        public Point eye_left { get; set; }

        public Point eye_right { get; set; }

        public Point mouth_left { get; set; }

        public Point mouth_right { get; set; }

        public Point nose { get; set; }

        public double height { get; set; }

        public double width { get; set; }
    }

    public class SearchCandidate : Face
    {
        public double similarity { get; set; }

    }

    public class IdentifyCandidate : Person
    {
        public double canfidence { get; set; }
    }

    public class ComponentSimilarity
    {
        public double eye { get; set; }
        public double mouth { get; set; }
        public double nose { get; set; }
        public double eyebrow { get; set; }
    }

    public class Face
    {
        public string face_id { get; set; }

        public string tag { get; set; }

    }

    public class FaceWithPosition : Face
    {
        public Position position { get; set; }
    }

    public class Group
    {
        public string group_id { get; set; }

        public string tag { get; set; }

        public string group_name { get; set; }
    }

    public class Person
    {
        public string person_id { get; set; }

        public string tag { get; set; }

        public string person_name { get; set; }
    }

    public class FaceSet
    {
        public string faceset_id { get; set; }

        public string tag { get; set; }

        public string faceset_name { get; set; }
    }

    public class Image
    {
        public string img_id { get; set; }

        public string url { get; set; }
    }
}
