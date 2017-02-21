using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuredAPI.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace InsuredAPI.Data
{
    public class ZohoCRM
    {

        public static string zohoCrmUrl = "https://crm.zoho.com/crm/private/json/";
        public static string zohoCrmUriAccount = "Accounts/searchRecords?authtoken={0}&scope=crmapi&criteria=(E-mail:{1})";
        public static string zohoCrmUriFile = "Accounts/downloadFile?authtoken={0}&scope=crmapi&id={1}";
        public static string zohoCrmUriFiles = "Attachments/getRelatedRecords?authtoken={0}&newFormat=1&scope=crmapi&parentModule=Accounts&id={1}";
        public static string token = "c9e5f4859296401b140f39906774892b";
        public static string attachmentsPath = "C:\\Reports\\";

        public List<Policy> GetPolicies(string email)
        {
            try
            {
                string _serviceAddress = string.Format(zohoCrmUrl + zohoCrmUriAccount, token, email);

                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(_serviceAddress).GetAwaiter().GetResult();
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Account.ZohoAccounts account = JsonConvert.DeserializeObject<Account.ZohoAccounts>(result);
                    return ParseData(account);
                }
            }
            catch (Exception es) {
                throw es;
            }
        }

        public List<InsuredAPI.Model.Attachment> GetPolicyAttachments(string accountId)
        {
            try
            {
                string _serviceAddress = string.Format(zohoCrmUrl + zohoCrmUriFiles, token, accountId);

                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(_serviceAddress).GetAwaiter().GetResult();
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Attachment.ZohoAttachment attachments = JsonConvert.DeserializeObject<Attachment.ZohoAttachment>(result);
                    return ParseDataAttachment(attachments, accountId);
                }
            }
            catch (Exception es)
            {
                throw es;
            }
        }

        private string GetImage(string policyType) {
            switch (policyType) {
                case "Personal Auto":
                case "Car Rental Liability":
                case "Commercial Auto":
                    return "http://www.mami-web.com/app/img/auto.png";
                case "HO3":
                case "HO3 X Wind":
                case "HO4":
                case "HO4 X Wind":
                case "HO5":
                case "HO5 X Wind":
                case "HO6":
                case "HO6 X Wind":
                case "HO8":
                case "Home Owner":
                case "HW2":
                case "HW4":
                case "HW6":
                case "Wind Only-Homeowners":
                case "Ex - Wind - Homeowners":
                    return "http://www.mami-web.com/app/img/home.png";
                case "Travel":
                    return "http://www.mami-web.com/app/img/travel.png";
                case "Accident Insurance":
                case "Animal Liability":
                case "Aviation":
                case "Boiler and Machinery-EQ":
                case "Bond":
                case "BR X - Wind":
                case "Builder's Risk":
                case "Cargo":
                case "Commercial General Liability":
                case "Commercial Non - Residential Wind":
                case "Commercial Package":
                case "Commercial Property":
                case "Commercial Residential Wind":
                case "Commercial Umbrella":
                case "Comprehensive Personal Liability":
                case "Consultation":
                case "Crime":
                case "Cyber, E & O, GL":
                case "D & O":
                case "Dental":
                case "Disability":
                case "DP1":
                case "DP3":
                case "DP3 X Wind":
                case "DW2":
                case "E & O":
                case "Earthquake":
                case "EPLI":
                case "Excess Flood":
                case "Excess Umbrella":
                case "Flood":
                case "Franchise":
                case "Premium Financed":
                case "Glass":
                case "Group Health":
                case "Group Voluntary":
                case "Individual Health":
                case "K & R":
                case "Life":
                case "Liquor Liability":
                case "Long Term Care":
                case "Mobile Home":
                case "Mortgagee Interest":
                case "Negotiation":
                case "Patient Advocacy":
                case "Personal Articles Floater":
                case "Personal Motorcyle":
                case "Personal Umbrella":
                case "Premium Financing":
                case "Renters":
                case "Service America":
                case "Short Term Health":
                case "Special Event":
                case "Wind Mitigation Inspections":
                case "Wind Only-Commercial":
                case "Workmans Comp":
                case "Yacht / Boat":
                case "Vision":
                case "Lessors Risk":
                case "Financing":
                default:
                    return "http://www.mami-web.com/app/img/doc.png";
            }
        }

        private List<Policy> ParseData(Account.ZohoAccounts data) {
            List<Policy> policies = new List<Policy>();
            foreach (Account.Row r in data.response.result.Accounts.row) {
                Policy p = new Policy();
                foreach (Account.FL f in r.FL) {
                    switch (f.val) {
                        case "ACCOUNTID":
                            p.id = f.content;
                            break;
                        case "Account Type":
                            p.type = f.content;
                            break;
                        case "Effective Date":
                            p.effective = DateTime.Parse(f.content);
                            break;
                        case "Expiration Date":
                            p.expiration = DateTime.Parse(f.content);
                            break;
                        case "Insurer":
                            p.insurer = f.content;
                            break;
                        case "Policy Number":
                            p.policy_number = f.content;
                            break;
                        case "Agent":
                            p.agent = f.content;
                            break;
                        case "Total Carrier Premium":
                            p.premium = f.content;
                            break;
                    }
                    p.image = GetImage(p.type);
                }
                policies.Add(p);
            }
            return policies;

        }

        private List<InsuredAPI.Model.Attachment> ParseDataAttachment(Attachment.ZohoAttachment data, string accountId)
        {
            List<InsuredAPI.Model.Attachment> attachments = new List<InsuredAPI.Model.Attachment>();
            foreach (Attachment.Row r in data.response.result.Attachments.row)
            {
                InsuredAPI.Model.Attachment a = new InsuredAPI.Model.Attachment();
                foreach (Attachment.FL f in r.FL)
                {
                    switch (f.val)
                    {
                        case "id":
                            a.id = f.content;
                            break;
                        case "File Name":
                            a.name = f.content;
                            break;
                        case "Size":
                            a.size = int.Parse(f.content);
                            break;
                        case "Modified Time":
                            a.modified = DateTime.Parse(f.content);
                            break;
                        case "Attached By":
                            a.modifiedBy = f.content;
                            break;
                    }
                }
                attachments.Add(a);
                //SaveFile(a);
                DoStuff(a, accountId);

            }
            return attachments;

        }

        private void SaveFile(InsuredAPI.Model.Attachment file) {
            try
            {
                string _serviceAddress = string.Format(zohoCrmUrl + zohoCrmUriFile, token, file.id);

                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(_serviceAddress).GetAwaiter().GetResult();
                    //var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                    //byte[] array = Encoding.ASCII.GetBytes(result);
                    File.WriteAllBytes("C:\\Reports\\"+ file.name, result);

                }
            }
            catch (Exception es)
            {
                //throw es;
            }
        }
        
        public async Task DoStuff(InsuredAPI.Model.Attachment file, string accountId)
        {
            await Task.Run(() =>
            {
                LongRunningOperation(file, accountId);
            });
        }

        private static async Task LongRunningOperation(InsuredAPI.Model.Attachment file, string accountId)
        {
            try
            {
                string _serviceAddress = string.Format(zohoCrmUrl + zohoCrmUriFile, token, file.id);

                using (HttpClient httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(_serviceAddress).GetAwaiter().GetResult();
                    //var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                    //byte[] array = Encoding.ASCII.GetBytes(result);

                    string path = attachmentsPath + accountId + "\\";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    File.WriteAllBytes(path + file.name, result);

                }
            }
            catch (Exception es)
            {
                //throw es;
            }
        }
        
    }

}

