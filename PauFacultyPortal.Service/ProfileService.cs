using System.Collections.Generic;
using System.Linq;
using PauFacultyPortal.Models;
using PauFacultyPortal.ViewModel.Profile;


namespace PauFacultyPortal.Service
{
    public class ProfileService
    {
        PauFacultyPortalEntities _db = new PauFacultyPortalEntities();
        public List<ProfileViewModel> GetProfileInfo(string userId)
        {
            List<ProfileViewModel> list = new List<ProfileViewModel>();
            ProfileViewModel modelProfile = new ProfileViewModel();
            if (!string.IsNullOrEmpty(userId))
            {


                var accountList = _db.Accounts.Where(x => x.LoginIdentity == userId).FirstOrDefault();

                var metaInfoItem = _db.AccountMetaInformations.Where(y => y.AccountId == accountList.AccountId).FirstOrDefault();


                var educationList = _db.AccountExtEducations.Where(x => x.AccountId == accountList.AccountId).ToList();


                var metaProfessionals = _db.AccountMetaProfessionals.Where(x => x.AccountId == accountList.AccountId).ToList();

                var professionalActivities = _db.AccountExtProfessionalActivities.Where(x => x.AccountId == accountList.AccountId).ToList();

                var projects = _db.AccountExtProjects.Where(x => x.AccountId == accountList.AccountId).ToList();



                if (accountList != null)
                {


                    string userpic = "http://123.136.27.58/umsapi/api/ProfileImageTransferService?imageName=" + accountList.LoginIdentity.ToString() + ".jpg&type=1";


                    List<ProfileEducationViewModel> educationModelList = new List<ProfileEducationViewModel>();
                    if (educationList != null)
                    {
                        foreach (var educationItem in educationList)
                        {
                            ProfileEducationViewModel education = new ProfileEducationViewModel()
                            {

                                AccountExtEducationId = educationItem.AccountExtEducationId,
                                NameOfExamination = educationItem.NameOfExamination,
                                PassingYear = educationItem.NameOfExamination,
                                Result = educationItem.Result,
                                StartingSession = educationItem.Result,
                                SubjectStudied = educationItem.SubjectStudied,
                                UniversityBoard = educationItem.UniversityBoard
                            };


                            educationModelList.Add(education);
                        }
                    }


                    ProfileMetainfoViewModel ProfileMetainfoModel = new ProfileMetainfoViewModel();

                    if (metaInfoItem != null)
                    {
                         ProfileMetainfoModel = new ProfileMetainfoViewModel()
                        {

                            AccountMetaInformationId = metaInfoItem.AccountMetaInformationId,
                            FatherName = metaInfoItem.FatherName,
                            MothersName = metaInfoItem.MothersName,
                            MaritalStatusId = metaInfoItem.MaritalStatusId,
                            SpouceName = metaInfoItem.SpouceName,
                            BloodGroupsId = metaInfoItem.BloodGroupsId,
                            GenderId = metaInfoItem.GenderId,
                            CurrentAddress = metaInfoItem.CurrentAddress,
                            PermanentAddress = metaInfoItem.PermanentAddress,
                            DateOfBirth = metaInfoItem.DateOfBirth,
                            JoiningDateTime = metaInfoItem.JoiningDateTime

                        };

                    }


                    


                    List<ProfileMetaProfesionalViewModel> metaProfessionalList = new List<ProfileMetaProfesionalViewModel>();

                    if (metaProfessionals != null)
                    {
                        foreach (var mpItem in metaProfessionals)
                        {
                            ProfileMetaProfesionalViewModel metaProfessionalmodel = new ProfileMetaProfesionalViewModel()
                            {

                                AccountMetaProfessionalId = mpItem.AccountMetaProfessionalId,
                                BackGround = mpItem.BackGround,
                                MediaContriBution = mpItem.MediaContriBution,
                                PhdSuperVision = mpItem.PhdSuperVision,
                                Publication = mpItem.Publication,
                                ResearchInterest = mpItem.ResearchInterest
                            };

                            metaProfessionalList.Add(metaProfessionalmodel);

                        }
                    }
                    

                    List<ProfileProfesionalActivityViewModel> profesionalActivityList = new List<ProfileProfesionalActivityViewModel>();
                    if (professionalActivities  != null)
                    {
                        foreach (var paItem in professionalActivities)
                        {
                            ProfileProfesionalActivityViewModel paModel = new ProfileProfesionalActivityViewModel()
                            {
                                AccountExtProfessionalActivityId = paItem.AccountExtProfessionalActivityId,
                                AttendType = paItem.AttendType,
                                DateOfActivity = paItem.DateOfActivity,
                                Description = paItem.Description,
                                Location = paItem.Location

                            };

                            profesionalActivityList.Add(paModel);
                        }
                    }

                    
                    List<ProfileProjectViewModel> projectList = new List<ProfileProjectViewModel>();

                    if (projects != null)
                    {
                        foreach (var projectItem in projects)
                        {
                            ProfileProjectViewModel project = new ProfileProjectViewModel()
                            {
                                AccountExtProjectId = projectItem.AccountExtProjectId,
                                ProjectCompletedDate = projectItem.ProjectCompletedDate,
                                ProjectStratedDate = projectItem.ProjectStratedDate,
                                ProjectTitle = projectItem.ProjectTitle,
                                project_Location = projectItem.ProjectTitle,
                                ShortDescription = projectItem.ShortDescription
                            };

                            projectList.Add(project);
                        }
                    }

                    


                    modelProfile = new ProfileViewModel()
                    {
                        AccountId = accountList.AccountId,
                        Name = accountList.Name,
                        Email = accountList.Email,
                        PhoneNo = accountList.PhoneNo,
                        TeleExchange = accountList.TeleExchange,
                        Designation = accountList.Designation,
                        DepartmentSection = accountList.DepartmentSection,
                        LoginIdentity = accountList.LoginIdentity,
                        Password = accountList.Password,
                        AccountsRoleId = accountList.AccountsRoleId,
                        Pic = userpic,
                        Status = accountList.Status,
                        PartimeTeacher = accountList.PartimeTeacher,
                        ExpireDate = accountList.ExpireDate,
                        Deactivate = accountList.Deactivate,
                        CauseOfDeactivate = accountList.CauseOfDeactivate,
                        TeporaryBlock = accountList.TeporaryBlock,
                        TemporaryBlockExpireDate = accountList.TemporaryBlockExpireDate,
                        CreatedDate = accountList.CreatedDate,
                        LastPsswordChange = accountList.LastPsswordChange,
                        ProfileEducation = educationModelList,
                        ProfileMetainformation = ProfileMetainfoModel,
                        ProfileMetaProfessional = metaProfessionalList,
                        ProfileProfessionalActivity = profesionalActivityList,
                        ProfileProject = projectList

                    };


                     list.Add(modelProfile);
                }

            }


            return list;
        }

    }
}
