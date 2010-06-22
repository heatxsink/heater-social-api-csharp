// The MIT License
// 
// Copyright (c) 2010 Nicholas A. Granado
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to 
// deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using Jayrock.Json.Conversion;

namespace Heater.Social.MySpace.DataContracts.OpenSocial
{
    public class DetailedPerson
    {
        [JsonMemberName("aboutMe")]
        public string AboutMe { set; get; }

        [JsonMemberName("accounts")]
        public Account Account { set; get; }

        [JsonMemberName("activities")]
        public string[] Activities { set; get; }

        [JsonMemberName("addresses")]
        public Address[] Addresses { set; get; }

        [JsonMemberName("age")]
        public string Age { get; set; }

        [JsonMemberName("anniversary")]
        public string Anniversary { get; set; }

        [JsonMemberName("birthday")]
        public string Birthday { get; set; }

        [JsonMemberName("bodyType")]
        public BodyType BodyType { get; set; }

        [JsonMemberName("books")]
        public string[] Books { get; set; }

        [JsonMemberName("cars")]
        public string[] Cars { get; set; }

        [JsonMemberName("children")]
        public string[] Children { get; set; }

        [JsonMemberName("connected")]
        public Presence Connected { get; set; }

        [JsonMemberName("currentLocation")]
        public Address CurrentLocation { get; set; }

        [JsonMemberName("displayName")]
        public string DisplayName { get; set; }

        [JsonMemberName("drinker")]
        public Drinker Drinker { get; set; }

        [JsonMemberName("emails")]
        public PluralPersonField[] Emails { get; set; }

        [JsonMemberName("ethnicity")]
        public string Ethnicity { get; set; }

        [JsonMemberName("fashion")]
        public string Fashion { get; set; }

        [JsonMemberName("food")]
        public string[] Food { get; set; }

        [JsonMemberName("gender")]
        public string Gender { get; set; }

        [JsonMemberName("happiestWhen")]
        public string HappiestWhen { get; set; }

        [JsonMemberName("hasApp")]
        public string HasApp { get; set; }

        [JsonMemberName("heroes")]
        public string[] Heroes { get; set; }

        [JsonMemberName("humor")]
        public string Humor { get; set; }

        [JsonMemberName("id")]
        public string Id { get; set; }

        [JsonMemberName("ims")]
        public PluralPersonField[] Ims { get; set; }

        [JsonMemberName("interests")]
        public string[] Interests { get; set; }

        [JsonMemberName("jobInterests")]
        public string JobInterests { get; set; }

        [JsonMemberName("languagesSpoken")]
        public string[] LanguagesSpoken { get; set; }

        [JsonMemberName("livingArrangement")]
        public string LivingArrangement { get; set; }

        [JsonMemberName("lookingFor")]
        public LookingFor[] LookingFor { get; set; }

        [JsonMemberName("movies")]
        public string[] Movies { get; set; }

        [JsonMemberName("music")]
        public string[] Music { get; set; }

        [JsonMemberName("name")]
        public DetailedName Name { get; set; }

        [JsonMemberName("networkPresence")]
        public NetworkPresence NetworkPresence { get; set; }

        [JsonMemberName("nickname")]
        public string Nickname { get; set; }

        [JsonMemberName("organizations")]
        public Organization[] Organizations { get; set; }

        [JsonMemberName("pets")]
        public string Pets { get; set; }

        [JsonMemberName("phoneNumbers")]
        public PluralPersonField[] PhoneNumbers { get; set; }

        [JsonMemberName("photos")]
        public PluralPersonField[] Photos { get; set; }

        [JsonMemberName("politicalViews")]
        public string PoliticalViews { get; set; }

        [JsonMemberName("preferredUsername")]
        public string PreferredUsername { get; set; }

        [JsonMemberName("profileSong")]
        public Url ProfileSong { get; set; }

        [JsonMemberName("profileUrl")]
        public string ProfileUrl { get; set; }

        [JsonMemberName("profileVideo")]
        public Url ProfileVideo { get; set; }

        [JsonMemberName("published")]
        public string Published { get; set; }

        [JsonMemberName("quotes")]
        public string[] Quotes { get; set; }

        [JsonMemberName("relationships")]
        public string[] Relationships { get; set; }

        [JsonMemberName("relationshipStatus")]
        public string RelationshipStatus { get; set; }

        [JsonMemberName("religion")]
        public string Religion { get; set; }

        [JsonMemberName("romance")]
        public string Romance { get; set; }

        [JsonMemberName("scaredOf")]
        public string ScaredOf { get; set; }

        [JsonMemberName("sexualOrientation")]
        public string SexualOrientation { get; set; }

        [JsonMemberName("smoker")]
        public Smoker Smoker { get; set; }

        [JsonMemberName("sports")]
        public string[] Sports { get; set; }

        [JsonMemberName("status")]
        public string Status { get; set; }

        [JsonMemberName("tags")]
        public string[] Tags { get; set; }

        [JsonMemberName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonMemberName("turnOffs")]
        public string[] TurnOffs { get; set; }

        [JsonMemberName("turnOns")]
        public string[] TurnOns { get; set; }

        [JsonMemberName("tvShows")]
        public string[] TvShows { get; set; }

        [JsonMemberName("updated")]
        public string Updated { get; set; }

        [JsonMemberName("urls")]
        public Url[] Urls { get; set; }

        [JsonMemberName("utcOffset")]
        public string UtcOffset { get; set; }

        [JsonMemberName("msLargeImage")]
        public string LargeImage { get; set; }

        [JsonMemberName("msMediumImage")]
        public string MediumImage { get; set; }

        [JsonMemberName("msMood")]
        public string Mood { get; set; }

        [JsonMemberName("msMoodLastUpdated")]
        public string MoodLastUpdated { get; set; }

        [JsonMemberName("msZodiacSign")]
        public string ZodiacSign { get; set; }
    }
}
