#region copyright
/*MIT License

Copyright (c) 2015-2017 XaKO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion
using System;
using System.Windows.Media;

namespace ESO_Assistant
{
    class UserStatus
    {
        private int FStatus;
        private string FNick;
        private Color FOnline;
        private string FLastLogin;
        private string FLastUpdate;
        private string FClan;
        private string FClanDescription;
        private string FPRS;
        private string FPRD;
        private string FPRYS;
        private string FPRYT;
        private string FPRYD;
        private string FAvatar;
        private string FIcon;
        private string FormatPR(string PR)
        {
            if (int.TryParse(PR, out int Rating))
            {
                if (Rating < 3)
                    return "Conscript" + " (Level " + Rating.ToString() + ")";
                if (Rating > 2 && Rating < 8)
                    return "Private" + " (Level " + Rating.ToString() + ")";
                if (Rating > 7 && Rating < 11)
                    return "Lance Corporal" + " (Level " + Rating.ToString() + ")";
                if (Rating > 10 && Rating < 14)
                    return "Corporal" + " (Level " + Rating.ToString() + ")";
                if (Rating > 13 && Rating < 17)
                    return "Sergeant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 16 && Rating < 20)
                    return "Master Sergeant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 19 && Rating < 23)
                    return "2nd Lieutenant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 22 && Rating < 26)
                    return "1st Lieutenant" + " (Level " + Rating.ToString() + ")";
                if (Rating > 25 && Rating < 29)
                    return "Captain" + " (Level " + Rating.ToString() + ")";
                if (Rating > 28 && Rating < 32)
                    return "Major" + " (Level " + Rating.ToString() + ")";
                if (Rating > 31 && Rating < 35)
                    return "Lieutenant Colonel" + " (Level " + Rating.ToString() + ")";
                if (Rating > 34 && Rating < 38)
                    return "Colonel" + " (Level " + Rating.ToString() + ")";
                if (Rating > 37 && Rating < 41)
                    return "Brigadier" + " (Level " + Rating.ToString() + ")";
                if (Rating > 40 && Rating < 44)
                    return "Major General" + " (Level " + Rating.ToString() + ")";
                if (Rating > 43 && Rating < 47)
                    return "Lieutenant General" + " (Level " + Rating.ToString() + ")";
                if (Rating > 46 && Rating < 50)
                    return "General" + " (Level " + Rating.ToString() + ")";
                if (Rating > 49)
                    return "Field Marshal" + " (Level " + Rating.ToString() + ")";

            }
            return PR;
        }
        private string GetAvatarFromID(string ID)
        {
            if (ID == "8bf82e325d864aab8fb6bc227a09f226")
                return "pack://application:,,,/Avatars/avatar_tier1_01-sm.(0,0,4,1).jpg";

            if (ID == "3cb647657009445fbc06132c79baabab")
                return "pack://application:,,,/Avatars/avatar_tier1_02-sm.(0,0,4,1).jpg";

            if (ID == "4f33c4c6640a4dfcab6475cbcd94403f")
                return "pack://application:,,,/Avatars/avatar_tier1_03-sm.(0,0,4,1).jpg";

            if (ID == "718e486223a842c2ab6fd35307ef02b1")
                return "pack://application:,,,/Avatars/avatar_tier1_04-sm.(0,0,4,1).jpg";

            if (ID == "07c2cb652a1d4893b9a2d99f958e6d31")
                return "pack://application:,,,/Avatars/avatar_tier1_05-sm.(0,0,4,1).jpg";

            if (ID == "ec538ed0aa954cffb66e1e52d0ef3d64")
                return "pack://application:,,,/Avatars/avatar_tier1_06-sm.(0,0,4,1).jpg";

            if (ID == "0218ef8aad7d4b83aa94b7d3659e2337")
                return "pack://application:,,,/Avatars/avatar_tier1_07-sm.(0,0,4,1).jpg";

            if (ID == "96354d77298944bda2d2366259ed706e")
                return "pack://application:,,,/Avatars/avatar_tier1_08-sm.(0,0,4,1).jpg";

            if (ID == "7037F010793C44699ABDA6EF6E6CF895")
                return "pack://application:,,,/Avatars/avatar_tier1_09-sm.(0,0,4,1).jpg";

            if (ID == "0A6F27904D9E46e8A83B8651F2C084EE")
                return "pack://application:,,,/Avatars/avatar_tier1_10-sm.(0,0,4,1).jpg";

            if (ID == "1ED65B1AB7124f998DC32F921284B2C5")
                return "pack://application:,,,/Avatars/avatar_tier1_11-sm.(0,0,4,1).jpg";

            if (ID == "91E0744C321E4ebcB1F2BB61864C0ECC")
                return "pack://application:,,,/Avatars/avatar_tier1_12-sm.(0,0,4,1).jpg";

            if (ID == "A632EBB2CC2940099F60C79CE82A3782")
                return "pack://application:,,,/Avatars/avatar_tier1_13-sm.(0,0,4,1).jpg";

            if (ID == "6BF1CDA1CB9E4c53BC6B79E28899F117")
                return "pack://application:,,,/Avatars/avatar_tier1_14-sm.(0,0,4,1).jpg";

            if (ID == "4EDC2767117E4b058CB84EEC3108BCCE")
                return "pack://application:,,,/Avatars/avatar_tier1_15-sm.(0,0,4,1).jpg";

            if (ID == "571A6711431541158F911C607CBE4F64")
                return "pack://application:,,,/Avatars/avatar_tier1_16-sm.(0,0,4,1).jpg";

            if (ID == "6983279CC5FA496f9128E3CD50BA48F7")
                return "pack://application:,,,/Avatars/avatar_tier1_17-sm.(0,0,4,1).jpg";

            if (ID == "69060A7953A847d5AC1B310CC796C4EC")
                return "pack://application:,,,/Avatars/avatar_tier1_18-sm.(0,0,4,1).jpg";

            if (ID == "53A2885492E34794854C5DEF656A2242")
                return "pack://application:,,,/Avatars/avatar_tier1_19-sm.(0,0,4,1).jpg";

            if (ID == "E0973385606549fe80D8749AE138860E")
                return "pack://application:,,,/Avatars/avatar_tier1_20-sm.(0,0,4,1).jpg";

            if (ID == "453F555167B04d1cBD4F7524BB3D3DA8")
                return "pack://application:,,,/Avatars/avatar_tier2_01-sm.(0,0,4,1).jpg";

            if (ID == "E58738AC49B244308348AEF89F7A1D74")
                return "pack://application:,,,/Avatars/avatar_tier2_02-sm.(0,0,4,1).jpg";

            if (ID == "F4360548A23E47a7B99655034A142AB7")
                return "pack://application:,,,/Avatars/avatar_tier2_03-sm.(0,0,4,1).jpg";

            if (ID == "393D02B47D2F493fBD74169B5AD46782")
                return "pack://application:,,,/Avatars/avatar_tier2_04-sm.(0,0,4,1).jpg";

            if (ID == "117BBDB1A97F4909B063B9CFD6AEF5C0")
                return "pack://application:,,,/Avatars/avatar_tier2_05-sm.(0,0,4,1).jpg";

            if (ID == "2F02B13EF0414c5cA65985FF6124F617")
                return "pack://application:,,,/Avatars/avatar_tier2_06-sm.(0,0,4,1).jpg";

            if (ID == "E642DA47766D4c76B2ACE88293D7C5E7")
                return "pack://application:,,,/Avatars/avatar_tier2_07-sm.(0,0,4,1).jpg";

            if (ID == "99919C71EE364f96970C92BA01C3BCFD")
                return "pack://application:,,,/Avatars/avatar_tier2_08-sm.(0,0,4,1).jpg";

            if (ID == "1E1354A9EC4C4873A204AB80FEB06DC2")
                return "pack://application:,,,/Avatars/avatar_tier2_09-sm.(0,0,4,1).jpg";

            if (ID == "2D4E8F6255F24af882646CF6DB059A8E")
                return "pack://application:,,,/Avatars/avatar_tier2_10-sm.(0,0,4,1).jpg";

            if (ID == "2808F16EFAE7425cB6EF551F69CE747A")
                return "pack://application:,,,/Avatars/avatar_tier2_11-sm.(0,0,4,1).jpg";

            if (ID == "DAB93CF1BB4F4ca9908500C56967F121")
                return "pack://application:,,,/Avatars/avatar_tier2_12-sm.(0,0,4,1).jpg";

            if (ID == "203969335E0048da911714FC6841D042")
                return "pack://application:,,,/Avatars/avatar_tier2_13-sm.(0,0,4,1).jpg";

            if (ID == "92D802EC707C4ca3B059347FA4770971")
                return "pack://application:,,,/Avatars/avatar_tier2_14-sm.(0,0,4,1).jpg";

            if (ID == "FFC3672624084b62BDF579D129C1357C")
                return "pack://application:,,,/Avatars/avatar_tier2_15-sm.(0,0,4,1).jpg";

            if (ID == "555564B6D5984f1aB1D7052E8EB03F3B")
                return "pack://application:,,,/Avatars/avatar_tier2_16-sm.(0,0,4,1).jpg";

            if (ID == "9DEB1EA8A4AF44d1932462FB086F4589")
                return "pack://application:,,,/Avatars/avatar_tier2_17-sm.(0,0,4,1).jpg";

            if (ID == "65085E77B9F34e17926D060A71ECDDEA")
                return "pack://application:,,,/Avatars/avatar_tier2_18-sm.(0,0,4,1).jpg";

            if (ID == "A4E28F5B734F4bd6BC7B0BEDFCB794B1")
                return "pack://application:,,,/Avatars/avatar_tier2_19-sm.(0,0,4,1).jpg";

            if (ID == "7775DA3F0B9C4f99915A0D3A03C3C47E")
                return "pack://application:,,,/Avatars/avatar_tier2_20-sm.(0,0,4,1).jpg";

            if (ID == "D78B62A673704fe4B728CBA8EECCDB39")
                return "pack://application:,,,/Avatars/avatar_tier2_21-sm.(0,0,4,1).jpg";

            if (ID == "CB9614DDEC584f13B316A7374DAAC8BC")
                return "pack://application:,,,/Avatars/avatar_tier2_22-sm.(0,0,4,1).jpg";

            if (ID == "056E7483851847cb80BE385D7B1CC73E")
                return "pack://application:,,,/Avatars/avatar_tier2_23-sm.(0,0,4,1).jpg";

            if (ID == "6F41B230195B4a53AEA974E0054F8381")
                return "pack://application:,,,/Avatars/avatar_tier2_24-sm.(0,0,4,1).jpg";

            if (ID == "1FE3245B419D4b9e8EFBE3C554FEE608")
                return "pack://application:,,,/Avatars/avatar_tier2_25-sm.(0,0,4,1).jpg";

            if (ID == "DD335A1A31B8491aAD29DC5AA8F300EA")
                return "pack://application:,,,/Avatars/avatar_tier2_26-sm.(0,0,4,1).jpg";

            if (ID == "883FA8D0B2AE42aa988DF45CF045D203")
                return "pack://application:,,,/Avatars/avatar_tier3_01-sm.(0,0,4,1).jpg";

            if (ID == "8816CF4DDAEA43bbB25B43BD80B0B4E3")
                return "pack://application:,,,/Avatars/avatar_tier3_02-sm.(0,0,4,1).jpg";

            if (ID == "EB0DA7CD545B41c1ADB5217DFCD7A1A4")
                return "pack://application:,,,/Avatars/avatar_tier3_03-sm.(0,0,4,1).jpg";

            if (ID == "5E89742837574e1195251E096C72975F")
                return "pack://application:,,,/Avatars/avatar_tier3_04-sm.(0,0,4,1).jpg";

            if (ID == "B3780CE346994f178167C03F21D5AF27")
                return "pack://application:,,,/Avatars/avatar_tier3_05-sm.(0,0,4,1).jpg";

            if (ID == "1DB54546B3474921B875743869FC7E72")
                return "pack://application:,,,/Avatars/avatar_tier3_06-sm.(0,0,4,1).jpg";

            if (ID == "49ACAD53D85844eb97BB701D31290BA4")
                return "pack://application:,,,/Avatars/avatar_tier3_07-sm.(0,0,4,1).jpg";

            if (ID == "DF66A29A174140c5AED343770399A36D")
                return "pack://application:,,,/Avatars/avatar_tier3_08-sm.(0,0,4,1).jpg";

            if (ID == "AD476EAE4798422a9DB2CD58BC192D2E")
                return "pack://application:,,,/Avatars/avatar_tier3_09-sm.(0,0,4,1).jpg";

            if (ID == "2695D251FEAC43a88C8DEB33F446DE01")
                return "pack://application:,,,/Avatars/avatar_tier3_10-sm.(0,0,4,1).jpg";

            if (ID == "23BD8CB8AADD4a1bB798D18F1AE8D2B5")
                return "pack://application:,,,/Avatars/avatar_tier3_11-sm.(0,0,4,1).jpg";

            if (ID == "29DCE1286B0B4b31A03D5EF92A333958")
                return "pack://application:,,,/Avatars/avatar_tier3_12-sm.(0,0,4,1).jpg";

            if (ID == "C4F44F57D68C4e338BC1848364A474FD")
                return "pack://application:,,,/Avatars/avatar_tier3_13-sm.(0,0,4,1).jpg";

            if (ID == "A4C0A7AB7C7041d5B6680997A33A071F")
                return "pack://application:,,,/Avatars/avatar_tier3_14-sm.(0,0,4,1).jpg";

            if (ID == "8F43A431AF48419080358B97BE9F9E40")
                return "pack://application:,,,/Avatars/avatar_tier3_15-sm.(0,0,4,1).jpg";

            if (ID == "494F56E8C3B341a596D8FB15E18A654B")
                return "pack://application:,,,/Avatars/avatar_tier3_16-sm.(0,0,4,1).jpg";

            if (ID == "502BE28D06EF41379C5F5635820B6929")
                return "pack://application:,,,/Avatars/avatar_tier3_17-sm.(0,0,4,1).jpg";

            if (ID == "CC3C8BEEC9054a20B1665CADE1D4089A")
                return "pack://application:,,,/Avatars/avatar_tier3_18-sm.(0,0,4,1).jpg";

            if (ID == "A9199968CDBB4f338687FA43AE943264")
                return "pack://application:,,,/Avatars/avatar_tier3_19-sm.(0,0,4,1).jpg";

            if (ID == "BCF28F8B25BE4d17850920EE23EF0BD6")
                return "pack://application:,,,/Avatars/avatar_tier3_20-sm.(0,0,4,1).jpg";

            if (ID == "30BE325A2D4B4fd0B38A69DF0F253E27")
                return "pack://application:,,,/Avatars/avatar_tier3_21-sm.(0,0,4,1).jpg";

            if (ID == "E888B33E303249419B0795F4AE67AB45")
                return "pack://application:,,,/Avatars/avatar_tier3_22-sm.(0,0,4,1).jpg";

            if (ID == "987EB6A563774309A5DD51E8F5A81C62")
                return "pack://application:,,,/Avatars/avatar_tier3_23-sm.(0,0,4,1).jpg";

            if (ID == "B1F3C0149F294af78530E0299384A2A0")
                return "pack://application:,,,/Avatars/avatar_tier3_24-sm.(0,0,4,1).jpg";

            if (ID == "8EAF68F6EA4440fa954C79CDA4A0301D")
                return "pack://application:,,,/Avatars/avatar_tier3_25-sm.(0,0,4,1).jpg";

            if (ID == "50AEA6ABAB664a0d82C5BC3BFB4D6347")
                return "pack://application:,,,/Avatars/avatar_tier3_26-sm.(0,0,4,1).jpg";

            if (ID == "C223306B63CA4a7f8B75800346F2C5D3")
                return "pack://application:,,,/Avatars/avatar_tier3_27-sm.(0,0,4,1).jpg";

            if (ID == "AD9F800F4ACA4391A7309A03EEC0A8E9")
                return "pack://application:,,,/Avatars/avatar_tier3_28-sm.(0,0,4,1).jpg";

            if (ID == "8C713428E6DD414cA50526A17420A140")
                return "pack://application:,,,/Avatars/avatar_tier3_29-sm.(0,0,4,1).jpg";

            if (ID == "DBCF1E781B454187BF7A4AAC0255041F")
                return "pack://application:,,,/Avatars/avatar_tier3_30-sm.(0,0,4,1).jpg";

            if (ID == "2a8fab35fd694ec7a6c7c14794b457eb")
                return "pack://application:,,,/Avatars/avatarX1-sm.(0,0,4,1).jpg";

            if (ID == "29015d3b05a74a9bb3bf68dd242334a6")
                return "pack://application:,,,/Avatars/avatarX2-sm.(0,0,4,1).jpg";

            if (ID == "e35df47c1b4e448da3840e5259ffd311")
                return "pack://application:,,,/Avatars/avatarX3-sm.(0,0,4,1).jpg";

            if (ID == "5dd2c4059e4e4f1d960e35018ac8b26c")
                return "pack://application:,,,/Avatars/avatarX4-sm.(0,0,4,1).jpg";

            if (ID == "9619a60f601641d19e9a33810d78c4c7")
                return "pack://application:,,,/Avatars/avatarX5-sm.(0,0,4,1).jpg";

            if (ID == "c49e19280aa548bb9bfc6d5089f66743")
                return "pack://application:,,,/Avatars/avatarX6-sm.(0,0,4,1).jpg";

            if (ID == "cd4be3e0b51b429787d0ad7c0ce1b0fe")
                return "pack://application:,,,/Avatars/avatarX7-sm.(0,0,4,1).jpg";

            if (ID == "0a4349e3cb964366aea4959997f916d9")
                return "pack://application:,,,/Avatars/avatarX8-sm.(0,0,4,1).jpg";

            if (ID == "8a844f16769a4d49b7fa6103ccc9a1f0")
                return "pack://application:,,,/Avatars/avatarX9-sm.(0,0,4,1).jpg";

            if (ID == "f0e3b33595d044a0a1cae7b1be18c9ea")
                return "pack://application:,,,/Avatars/avatarX10-sm.(0,0,4,1).jpg";

            if (ID == "67094b986d244e209c8c284d7b7771fe")
                return "pack://application:,,,/Avatars/avatarX11-sm.(0,0,4,1).jpg";

            if (ID == "226c27899ff14e27af4ef306483153de")
                return "pack://application:,,,/Avatars/avatarX12-sm.(0,0,4,1).jpg";

            if (ID == "5ec3d08b58114cc390802dee80223251")
                return "pack://application:,,,/Avatars/avatarX13-sm.(0,0,4,1).jpg";

            if (ID == "c229aefa362a4f5393e5c1bd581912d1")
                return "pack://application:,,,/Avatars/avatarX14-sm.(0,0,4,1).jpg";

            if (ID == "80a8865664384ec0aa1db21a3e757608")
                return "pack://application:,,,/Avatars/avatarX15-sm.(0,0,4,1).jpg";

            if (ID == "a340e036956440e588f89eda1bb8c61d")
                return "pack://application:,,,/Avatars/avatarX16-sm.(0,0,4,1).jpg";

            if (ID == "23da8e7ee1bd41eaa0298ada6cd5e68b")
                return "pack://application:,,,/Avatars/avatarX17-sm.(0,0,4,1).jpg";

            if (ID == "e5f3f9ee6c0a49a9be29c12836cf4a13")
                return "pack://application:,,,/Avatars/avatarX18-sm.(0,0,4,1).jpg";

            if (ID == "35a1e06319c74383903fcbc4798832ea")
                return "pack://application:,,,/Avatars/avatarX19-sm.(0,0,4,1).jpg";

            if (ID == "db67c6899dd0469f89ee12dab6f90281")
                return "pack://application:,,,/Avatars/avatarX20-sm.(0,0,4,1).jpg";

            if (ID == "eccebb63e6784ac184fb02077b4e198f")
                return "pack://application:,,,/Avatars/avatarX21-sm.(0,0,4,1).jpg";

            if (ID == "f2939cd118bd4011986055fd040e07df")
                return "pack://application:,,,/Avatars/avatarX22-sm.(0,0,4,1).jpg";

            if (ID == "b201d5fb4f7f43cebac5a1a518033536")
                return "pack://application:,,,/Avatars/avatarX23-sm.(0,0,4,1).jpg";

            if (ID == "9e499080bde343438d3db1217458f835")
                return "pack://application:,,,/Avatars/avatarX24-sm.(0,0,4,1).jpg";

            if (ID == "aa4e4ea8cb644398a3216c6c8eefcdf3")
                return "pack://application:,,,/Avatars/avatarX25-sm.(0,0,4,1).jpg";

            if (ID == "698b74216a394aa9aaa708be5cf65b0c")
                return "pack://application:,,,/Avatars/avatarX26-sm.(0,0,4,1).jpg";

            if (ID == "764b464793674a518e88526cf4e77801")
                return "pack://application:,,,/Avatars/avatarX27-sm.(0,0,4,1).jpg";

            if (ID == "b34746fde9ae4d4488c8ad8a0fe3abc2")
                return "pack://application:,,,/Avatars/avatarX28-sm.(0,0,4,1).jpg";

            if (ID == "acb93a843e95409ba4f61bfb9b56b973")
                return "pack://application:,,,/Avatars/avatarX29-sm.(0,0,4,1).jpg";

            if (ID == "578f3a59dc7f40ba944a09d03cd6e9a9")
                return "pack://application:,,,/Avatars/avatarX30-sm.(0,0,4,1).jpg";

            if (ID == "0efdbb5b250e434485228b006d4b1c4c")
                return "pack://application:,,,/Avatars/avatarX31-sm.(0,0,4,1).jpg";

            if (ID == "578261e4ccc445bb8d1e488b2ea73347")
                return "pack://application:,,,/Avatars/avatarX32-sm.(0,0,4,1).jpg";

            if (ID == "3662b9c67eac494c93d31f77bf8e66f6")
                return "pack://application:,,,/Avatars/avatarX33-sm.(0,0,4,1).jpg";

            if (ID == "fb3d3bc888ed43a9ae7eec98c6cacba8")
                return "pack://application:,,,/Avatars/avatarX34-sm.(0,0,4,1).jpg";

            if (ID == "faeb7020e0b9412f9293f96fbafb4eae")
                return "pack://application:,,,/Avatars/avatarX35-sm.(0,0,4,1).jpg";

            if (ID == "90f5907eabbe40f3b20014b2a3007f12")
                return "pack://application:,,,/Avatars/avatarX36-sm.(0,0,4,1).jpg";

            if (ID == "f2e89069a7694bd58705943bc536c0f9")
                return "pack://application:,,,/Avatars/avatarX37-sm.(0,0,4,1).jpg";

            if (ID == "13288c65d272482fbe7d728c98b46038")
                return "pack://application:,,,/Avatars/avatarX38-sm.(0,0,4,1).jpg";

            if (ID == "642620e4b96946b69c9df09212783de2")
                return "pack://application:,,,/Avatars/avatarX39-sm.(0,0,4,1).jpg";

            if (ID == "8af12a39a2324c32ac860654a70cf4f1")
                return "pack://application:,,,/Avatars/avatarX40-sm.(0,0,4,1).jpg";

            if (ID == "7b1b131582e645fdade348c0b1dbaa39")
                return "pack://application:,,,/Avatars/avatarX41-sm.(0,0,4,1).jpg";

            if (ID == "3882cfd23ce74c3caa395f2660cbaff8")
                return "pack://application:,,,/Avatars/avatarX42-sm.(0,0,4,1).jpg";

            if (ID == "7d18a487d85b4d739ef2cd3575390bf8")
                return "pack://application:,,,/Avatars/avatarX43-sm.(0,0,4,1).jpg";

            if (ID == "bad6168cc6ad4283b6de10ab71a14388")
                return "pack://application:,,,/Avatars/avatarX44-sm.(0,0,4,1).jpg";

            if (ID == "0c3f004631d84155bfe3f5ffb42c994f")
                return "pack://application:,,,/Avatars/avatarX45-sm.(0,0,4,1).jpg";

            if (ID == "1d4ad522-6af1-415d-9bdf-6632f2f055ac")
                return "pack://application:,,,/Avatars/avatarY1-sm.(0,0,4,1).jpg";

            if (ID == "38fcd4ff-ed1c-4462-853c-4cf641e7aa94")
                return "pack://application:,,,/Avatars/avatarY2-sm.(0,0,4,1).jpg";

            if (ID == "ae0f56f3-b24f-4a82-afec-53911109a1e7")
                return "pack://application:,,,/Avatars/avatarY3-sm.(0,0,4,1).jpg";

            if (ID == "e77b9abd-9ad8-4a32-87ea-632fa47cf7b0")
                return "pack://application:,,,/Avatars/avatarY4-sm.(0,0,4,1).jpg";

            if (ID == "ee51b2d8-191a-406a-86eb-eb012c701eab")
                return "pack://application:,,,/Avatars/avatarY5-sm.(0,0,4,1).jpg";

            if (ID == "d8c9dc25-1e5d-41db-91d7-8be58431042d")
                return "pack://application:,,,/Avatars/avatarY6-sm.(0,0,4,1).jpg";

            if (ID == "c65aa1f7-43ec-4543-b21f-83a838d71ddb")
                return "pack://application:,,,/Avatars/avatarY7-sm.(0,0,4,1).jpg";

            if (ID == "88e7634a-ad5f-4a1b-977d-56de1b9271cf")
                return "pack://application:,,,/Avatars/avatarY8-sm.(0,0,4,1).jpg";

            if (ID == "7c76d9f5-c2ef-45fb-be7a-734da1371270")
                return "pack://application:,,,/Avatars/avatarY9-sm.(0,0,4,1).jpg";

            if (ID == "5d7e7128-4338-4644-8101-12ec012afe77")
                return "pack://application:,,,/Avatars/avatarY10-sm.(0,0,4,1).jpg";

            if (ID == "0b98a7d2-3088-48cf-9c8a-a2b1512a43d8")
                return "pack://application:,,,/Avatars/avatarY11-sm.(0,0,4,1).jpg";

            if (ID == "1378c867-bf9e-4088-8d4b-6da8c3e2f6c5")
                return "pack://application:,,,/Avatars/avatarY12-sm.(0,0,4,1).jpg";

            if (ID == "29d10719-b3b2-4b9e-8098-2064af3c911e")
                return "pack://application:,,,/Avatars/avatarY13-sm.(0,0,4,1).jpg";

            if (ID == "0e3f2fe4-0c79-4500-bb68-4fdb7f4c47b4")
                return "pack://application:,,,/Avatars/avatarY14-sm.(0,0,4,1).jpg";

            if (ID == "c53b642d-6f73-4e19-b224-b382d10cf160")
                return "pack://application:,,,/Avatars/avatarY15-sm.(0,0,4,1).jpg";

            if (ID == "d94a0fa6-0cf8-4de0-85be-6aa4e9c02034")
                return "pack://application:,,,/Avatars/avatarY16-sm.(0,0,4,1).jpg";

            if (ID == "d70ec3fb-a3c5-468c-b311-07994e1c1d8d")
                return "pack://application:,,,/Avatars/avatarY17-sm.(0,0,4,1).jpg";

            if (ID == "745a3e78-5d11-484b-b7c3-6775db79bcdc")
                return "pack://application:,,,/Avatars/avatarY18-sm.(0,0,4,1).jpg";

            if (ID == "ddd65676-58ad-4dc4-837d-5f4cecba1a3f")
                return "pack://application:,,,/Avatars/avatarY19-sm.(0,0,4,1).jpg";

            if (ID == "2094631f-011a-4ce6-9eac-7a5e8d866715")
                return "pack://application:,,,/Avatars/avatarY20-sm.(0,0,4,1).jpg";

            if (ID == "c467e531-eda9-46bf-9849-706a2f1b827f")
                return "pack://application:,,,/Avatars/avatarY21-sm.(0,0,4,1).jpg";

            if (ID == "3b8a41db-06d6-4847-bfd2-9f560250e20d")
                return "pack://application:,,,/Avatars/avatarY22-sm.(0,0,4,1).jpg";

            if (ID == "bef1c47d-49b7-474d-9427-fd09c6e03ad3")
                return "pack://application:,,,/Avatars/avatarY23-sm.(0,0,4,1).jpg";

            if (ID == "ce2cbd88-2f86-4939-85c3-1a06f7a18164")
                return "pack://application:,,,/Avatars/avatarY24-sm.(0,0,4,1).jpg";

            if (ID == "74bc7cbd-c4f1-4c76-b46c-f67b5d38ba4e")
                return "pack://application:,,,/Avatars/avatarY25-sm.(0,0,4,1).jpg";

            if (ID == "eda5e165-4d9b-423c-89ba-24142895a406")
                return "pack://application:,,,/Avatars/avatarY26-sm.(0,0,4,1).jpg";

            if (ID == "e08e656b-f064-4046-91de-db2f51a43676")
                return "pack://application:,,,/Avatars/avatarY27-sm.(0,0,4,1).jpg";

            if (ID == "4cd87d4f-4638-45f8-9b5e-799a94c1c925")
                return "pack://application:,,,/Avatars/avatarY28-sm.(0,0,4,1).jpg";

            if (ID == "14eefce3-80bf-49eb-99d2-295747760abf")
                return "pack://application:,,,/Avatars/avatarY29-sm.(0,0,4,1).jpg";

            if (ID == "9801b5b3-c9ba-4cf1-834b-0518e3810661")
                return "pack://application:,,,/Avatars/avatarY30-sm.(0,0,4,1).jpg";

            if (ID == "f8d40623-a83c-409d-a8b4-c549e22c972a")
                return "pack://application:,,,/Avatars/avatarY31-sm.(0,0,4,1).jpg";

            if (ID == "9fcf71ac-f40c-4b36-bb80-623caa1949f3")
                return "pack://application:,,,/Avatars/avatarY32-sm.(0,0,4,1).jpg";

            if (ID == "1caa08bd-3b59-4c5b-bbf0-5cec331d6e16")
                return "pack://application:,,,/Avatars/avatarY33-sm.(0,0,4,1).jpg";

            if (ID == "fc1a26d9-bde2-4cb5-b844-8ff18ef1838b")
                return "pack://application:,,,/Avatars/avatarY34-sm.(0,0,4,1).jpg";

            if (ID == "8cb00f49-4120-454d-b78b-d886e55a9d05")
                return "pack://application:,,,/Avatars/avatarY35-sm.(0,0,4,1).jpg";

            if (ID == "94f210bb-aaf7-4166-887a-2dafaa229dc6")
                return "pack://application:,,,/Avatars/avatarY36-sm.(0,0,4,1).jpg";

            if (ID == "5807abf7-2b57-4c33-8a84-449728f70f4c")
                return "pack://application:,,,/Avatars/avatarY37-sm.(0,0,4,1).jpg";

            if (ID == "ad3226b5-5da6-479c-b696-d1650da97fd6")
                return "pack://application:,,,/Avatars/avatarY38-sm.(0,0,4,1).jpg";

            if (ID == "8470e885-8887-457d-a489-be55f9f95055")
                return "pack://application:,,,/Avatars/avatarY39-sm.(0,0,4,1).jpg";

            if (ID == "6abbfff5-c4cd-461e-a65a-3a5efc919cc2")
                return "pack://application:,,,/Avatars/avatarY40-sm.(0,0,4,1).jpg";

            if (ID == "431d0124-0ef3-411d-999e-70257f99e63b")
                return "pack://application:,,,/Avatars/avatarY41-sm.(0,0,4,1).jpg";

            if (ID == "615bf7b3-4e96-4fa5-b378-e0080073d236")
                return "pack://application:,,,/Avatars/avatarY42-sm.(0,0,4,1).jpg";

            if (ID == "76b55472-b2d9-445e-ad1e-c4642103f860")
                return "pack://application:,,,/Avatars/avatarY43-sm.(0,0,4,1).jpg";

            if (ID == "e96b8a53-b61d-4c0c-b9c9-0e52bd8ac02d")
                return "pack://application:,,,/Avatars/avatarY44-sm.(0,0,4,1).jpg";

            if (ID == "c5d17f17-78b5-4b46-9a95-d78bc43272d8")
                return "pack://application:,,,/Avatars/avatarY45-sm.(0,0,4,1).jpg";

            if (ID == "ea6022fb-19ab-4ecb-ac3d-c4469429a71f")
                return "pack://application:,,,/Avatars/avatarY46-sm.(0,0,4,1).jpg";

            if (ID == "c21080a0-b345-474c-909b-04cb9afb718a")
                return "pack://application:,,,/Avatars/avatarY47-sm.(0,0,4,1).jpg";

            if (ID == "5a7ce673-634d-4a37-ad43-5545a0a9e9d1")
                return "pack://application:,,,/Avatars/avatarY48-sm.(0,0,4,1).jpg";

            if (ID == "746075d1-6254-4dec-9127-ce7ec73827da")
                return "pack://application:,,,/Avatars/avatarY49-sm.(0,0,4,1).jpg";

            if (ID == "9140248b-5545-4954-87a5-ab463fcc7ed6")
                return "pack://application:,,,/Avatars/avatarY50-sm.(0,0,4,1).jpg";

            if (ID == "b899b821-e586-4698-a21c-d598325dc8b5")
                return "pack://application:,,,/Avatars/avatarY51-sm.(0,0,4,1).jpg";

            if (ID == "0c182d86-f9e0-4208-8074-0ce427e40a84")
                return "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg";
            return "pack://application:,,,/Avatars/avatarY52-sm.(0,0,4,1).jpg";
        }
        public string Icon
        {
            get { return FIcon; }
        }
        public Color Online
        {
            get { return FOnline; }
        }
        public string LastLogin
        {
            get { return FLastLogin; }
        }
        public string LastUpdate
        {
            get { return FLastUpdate; }
        }
        public string Clan
        {
            get { return FClan; }
        }
        public string ClanDescription
        {
            get { return FClanDescription; }
        }
        public string PRS
        {
            get { return FPRS; }
        }
        public string PRD
        {
            get { return FPRD; }
        }
        public string PRYS
        {
            get { return FPRYS; }
        }
        public string PRYT
        {
            get { return FPRYT; }
        }
        public string PRYD
        {
            get { return FPRYD; }
        }
        public string Avatar
        {
            get { return FAvatar; }
        }
        public UserStatus(string Name)
        {
            FStatus = -1;
            FNick = Name;
        }
        public int Status
        {
            get { return FStatus; }
        }
        public string Nick
        {
            get { return FNick; }
        }
        private string Pars(string T_, string _T, string Text)
        {
            int a, b;
            string Result = "";
            if (T_ == "" || Text == "" || _T == "")
                return Result;
            a = Text.IndexOf(T_);
            if (a < 0)
                return Result;
            b = Text.IndexOf(_T, a + T_.Length);
            if (b >= 0)
                Result = Text.Substring(a + T_.Length, b - a - T_.Length);
            return Result;
        }
        public void Get(string Data)
        {
            try
            {
                if (Data.Contains("<error>Failed to find user</error>"))
                    FStatus = -1;
                else
                if (Pars("<presence>", "</presence>", Data) == "online")
                {
                    FStatus = 1;
                    FNick = Pars("<name>", "</name>", Data);
                    FIcon = "pack://application:,,,/Icons/1.png";
                }
                else
                if (Pars("<presence>", "</presence>", Data) == "offline")
                {
                    FStatus = 0;
                    FNick = Pars("<name>", "</name>", Data);
                    FIcon = "pack://application:,,,/Icons/0.png";
                }
                else
                {
                    FStatus = 0;
                    FIcon = "pack://application:,,,/Icons/0.png";
                }
                if (FStatus != -1)
                {
                    if (Pars("<presence>", "</presence>", Data) == "online")
                        FOnline = Colors.LimeGreen;
                    else
                        FOnline = Colors.Transparent;
                    FAvatar = GetAvatarFromID(Pars("<avatarId>", "</avatarId>", Data));
                    FClan = Pars("<clanAbbr>", "</clanAbbr>", Data).Replace("_", "__");
                    FClanDescription = Pars("<clanName>", "</clanName>", Data).Replace("_", "__");
                    FPRS = Pars("<s>", "</s>", Data);
                    FPRS = Pars("<skillLevel>", "</skillLevel>", FPRS);
                    FPRS = "Supremacy nilla: " + FormatPR(FPRS);
                    FPRD = Pars("<d>", "</d>", Data);
                    FPRD = Pars("<skillLevel>", "</skillLevel>", FPRD);
                    FPRD = "Deathmatch nilla: " + FormatPR(FPRD);

                    FPRYS = Pars("<sy>", "</sy>", Data);
                    FPRYS = Pars("<skillLevel>", "</skillLevel>", FPRYS);
                    FPRYS = "Supremacy: " + FormatPR(FPRYS);

                    FPRYT = Pars("<ty>", "</ty>", Data);
                    FPRYT = Pars("<skillLevel>", "</skillLevel>", FPRYT);
                    FPRYT = "Treaty: " + FormatPR(FPRYT);

                    FPRYD = Pars("<dy>", "</dy>", Data);
                    FPRYD = Pars("<skillLevel>", "</skillLevel>", FPRYD);
                    FPRYD = "Deathmatch: " + FormatPR(FPRYD);
                    FLastLogin = DateTime.Parse(Pars("<lastLogin>", "</lastLogin>", Data)).ToLocalTime().ToString();
                    FLastUpdate = DateTime.Parse(Pars("<LastUpdated>", "</LastUpdated>", Data)).ToLocalTime().ToString();
                }

            }
            catch { FStatus = 0; FIcon = "pack://application:,,,/Icons/0.png"; }
        }
    }
}
