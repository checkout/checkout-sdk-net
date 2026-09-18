using Checkout.Identities.AddressDocumentVerification.Responses;
using Checkout.Identities.Entities;
using Checkout.Identities.FaceAuthentication.Requests;
using Checkout.Identities.FaceAuthentication.Responses;
using Checkout.Identities.IdDocumentVerification.Responses;
using Checkout.Identities.IdentityVerification.Requests;
using Checkout.Identities.IdentityVerification.Responses;
using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
using Xunit;
using DocumentType = Checkout.Identities.Entities.DocumentType;

namespace Checkout.Identities
{
    /// <summary>
    /// Schema validation tests for the Identities domain types changed by swagger 2026-09-02.
    ///
    /// Covers the IdvPdf signed_url to pdf_report rename, the additive IDV/FAV verification
    /// fields, and the enum and IdvDocument additions that the swagger changelog does not report.
    /// </summary>
    public class IdentitiesSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // --------------------------------------------------------------------
        // Part C - IdvPdf: signed_url replaced by pdf_report
        // The previous spec declared both keys and the SDK modelled only
        // signed_url, so pdf_report never populated. signed_url is now gone
        // from the spec entirely.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializePdfReportOnIdentityVerificationReportResponse()
        {
            const string json = @"{ ""pdf_report"": ""https://example.com/report.pdf"" }";

            var result = (IdentityVerificationReportResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationReportResponse));

            result.ShouldNotBeNull();
            result.PdfReport.ShouldBe("https://example.com/report.pdf");
        }

        [Fact]
        public void ShouldDeserializePdfReportOnAddressDocumentVerificationReportResponse()
        {
            const string json = @"{ ""pdf_report"": ""https://example.com/adv-report.pdf"" }";

            var result = (AddressDocumentVerificationReportResponse)
                Serializer.Deserialize(json, typeof(AddressDocumentVerificationReportResponse));

            result.ShouldNotBeNull();
            result.PdfReport.ShouldBe("https://example.com/adv-report.pdf");
        }

        [Fact]
        public void ShouldDeserializePdfReportOnIdDocumentVerificationReportResponse()
        {
            const string json = @"{ ""pdf_report"": ""https://example.com/iddv-report.pdf"" }";

            var result = (IdDocumentVerificationReportResponse)
                Serializer.Deserialize(json, typeof(IdDocumentVerificationReportResponse));

            result.ShouldNotBeNull();
            result.PdfReport.ShouldBe("https://example.com/iddv-report.pdf");
        }

        [Fact]
        public void ShouldNotMapTheRemovedSignedUrlKeyOnReportResponses()
        {
            const string json = @"{ ""signed_url"": ""https://example.com/report.pdf"" }";

            var result = (IdentityVerificationReportResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationReportResponse));

            result.ShouldNotBeNull();
            result.PdfReport.ShouldBeNull();
        }

        [Fact]
        public void ShouldSerializePdfReportUsingTheSwaggerKey()
        {
            var response = new IdentityVerificationReportResponse { PdfReport = "https://example.com/report.pdf" };

            var json = Serializer.Serialize(response);

            json.ShouldContain("\"pdf_report\":");
            json.ShouldNotContain("\"signed_url\":");
        }

        // --------------------------------------------------------------------
        // Part E1 - risk_labels is now a typed enum (IdvRiskLabel), not a
        // bare string list. Present on ADV, FAV and IDV responses.
        // --------------------------------------------------------------------

        [Theory]
        [InlineData(RiskLabel.MultipleFacesDetected, "multiple_faces_detected")]
        [InlineData(RiskLabel.MccNotConfident, "mcc_not_confident")]
        [InlineData(RiskLabel.RiskyDocumentFormat, "risky_document_format")]
        public void ShouldRoundTripEachRiskLabelValue(RiskLabel label, string expected)
        {
            var response = new IdentityVerificationResponse { RiskLabels = new List<RiskLabel> { label } };

            var json = Serializer.Serialize(response);
            var deserialized = (IdentityVerificationResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationResponse));

            json.ShouldContain($"\"{expected}\"");
            deserialized.RiskLabels.ShouldNotBeNull();
            deserialized.RiskLabels.Count.ShouldBe(1);
            deserialized.RiskLabels[0].ShouldBe(label);
        }

        [Fact]
        public void ShouldDeserializeRiskLabelsOnAddressDocumentVerificationResponse()
        {
            const string json = @"{ ""risk_labels"": [""mcc_not_confident"", ""risky_document_format""] }";

            var result = (AddressDocumentVerificationResponse)
                Serializer.Deserialize(json, typeof(AddressDocumentVerificationResponse));

            result.ShouldNotBeNull();
            result.RiskLabels.Count.ShouldBe(2);
            result.RiskLabels[0].ShouldBe(RiskLabel.MccNotConfident);
            result.RiskLabels[1].ShouldBe(RiskLabel.RiskyDocumentFormat);
        }

        [Fact]
        public void ShouldDeserializeRiskLabelsOnFaceAuthenticationResponse()
        {
            const string json = @"{ ""risk_labels"": [""multiple_faces_detected""] }";

            var result = (FaceAuthenticationResponse)
                Serializer.Deserialize(json, typeof(FaceAuthenticationResponse));

            result.ShouldNotBeNull();
            result.RiskLabels.Count.ShouldBe(1);
            result.RiskLabels[0].ShouldBe(RiskLabel.MultipleFacesDetected);
        }

        // --------------------------------------------------------------------
        // Part E2 - certifications and verification_policy_version on the
        // identity verification responses. certifications nests two deep.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeCertificationsAndVerificationPolicyVersion()
        {
            const string json = @"{
                ""verification_policy_version"": ""2.1"",
                ""certifications"": [
                    {
                        ""type"": ""diatf"",
                        ""data"": {
                            ""gpg45_profile"": ""M1C"",
                            ""level_of_confidence"": ""high"",
                            ""right_to_work"": ""GRANTED""
                        }
                    }
                ]
            }";

            var result = (IdentityVerificationResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationResponse));

            result.ShouldNotBeNull();
            result.VerificationPolicyVersion.ShouldBe("2.1");
            result.Certifications.ShouldNotBeNull();
            result.Certifications.Count.ShouldBe(1);
            result.Certifications[0].Type.ShouldBe(CertificationType.Diatf);
            result.Certifications[0].Data.ShouldNotBeNull();
            result.Certifications[0].Data.Gpg45Profile.ShouldBe(Entities.Gpg45Profile.M1C);
            result.Certifications[0].Data.LevelOfConfidence.ShouldBe(Entities.LevelOfConfidence.High);
            result.Certifications[0].Data.RightToWork.ShouldBe("GRANTED");
        }

        [Fact]
        public void ShouldRoundTripCertificationsOnIdentityVerificationAndAttemptResponse()
        {
            var original = new IdentityVerificationAndAttemptResponse
            {
                VerificationPolicyVersion = "1.0",
                Certifications = new List<Certification>
                {
                    new Certification
                    {
                        Type = CertificationType.Diatf,
                        Data = new DiatfCertificationData
                        {
                            Gpg45Profile = Entities.Gpg45Profile.H1A,
                            LevelOfConfidence = Entities.LevelOfConfidence.Medium,
                            RightToWork = "GRANTED"
                        }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (IdentityVerificationAndAttemptResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationAndAttemptResponse));

            json.ShouldContain("\"verification_policy_version\":\"1.0\"");
            json.ShouldContain("\"gpg45_profile\":\"H1A\"");
            json.ShouldContain("\"level_of_confidence\":\"medium\"");
            deserialized.VerificationPolicyVersion.ShouldBe("1.0");
            deserialized.Certifications[0].Type.ShouldBe(CertificationType.Diatf);
            deserialized.Certifications[0].Data.Gpg45Profile.ShouldBe(Entities.Gpg45Profile.H1A);
            deserialized.Certifications[0].Data.LevelOfConfidence.ShouldBe(Entities.LevelOfConfidence.Medium);
            deserialized.Certifications[0].Data.RightToWork.ShouldBe("GRANTED");
        }

        [Theory]
        [InlineData("M1A")]
        [InlineData("M1C")]
        [InlineData("H1A")]
        public void ShouldRoundTripEachGpg45ProfileValue(string expected)
        {
            const string template = @"{ ""gpg45_profile"": ""VALUE"" }";
            var json = template.Replace("VALUE", expected);

            var result = (DiatfCertificationData)Serializer.Deserialize(json, typeof(DiatfCertificationData));

            result.ShouldNotBeNull();
            Serializer.Serialize(result).ShouldContain($"\"gpg45_profile\":\"{expected}\"");
        }

        // --------------------------------------------------------------------
        // Part E3 - declared_data gains birth_date everywhere, plus
        // phone_number, email and address on identity verifications only.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripBirthDateOnDeclaredData()
        {
            var original = new DeclaredData { Name = "Hannah Bret", BirthDate = "1994-10-15" };

            var json = Serializer.Serialize(original);
            var deserialized = (DeclaredData)Serializer.Deserialize(json, typeof(DeclaredData));

            json.ShouldContain("\"birth_date\":\"1994-10-15\"");
            deserialized.Name.ShouldBe("Hannah Bret");
            deserialized.BirthDate.ShouldBe("1994-10-15");
        }

        [Fact]
        public void ShouldRoundTripAllIdentityDeclaredDataProperties()
        {
            var original = new IdentityDeclaredData
            {
                Name = "Hannah Bret",
                BirthDate = "1994-10-15",
                Email = "hannah.bret@example.com",
                PhoneNumber = new PhoneNumber { CountryCode = "+33", Number = "5555550102" },
                Address = new IdvAddress
                {
                    AddressLine1 = "123 Main Street",
                    AddressLine2 = "Apt 4B",
                    City = "London",
                    State = "Greater London",
                    Zip = "SW1A 1AA",
                    Country = CountryCode.GB
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (IdentityDeclaredData)Serializer.Deserialize(json, typeof(IdentityDeclaredData));

            json.ShouldContain("\"country_code\":\"+33\"");
            json.ShouldContain("\"address_line1\":\"123 Main Street\"");
            deserialized.Name.ShouldBe("Hannah Bret");
            deserialized.BirthDate.ShouldBe("1994-10-15");
            deserialized.Email.ShouldBe("hannah.bret@example.com");
            deserialized.PhoneNumber.CountryCode.ShouldBe("+33");
            deserialized.PhoneNumber.Number.ShouldBe("5555550102");
            deserialized.Address.AddressLine1.ShouldBe("123 Main Street");
            deserialized.Address.AddressLine2.ShouldBe("Apt 4B");
            deserialized.Address.City.ShouldBe("London");
            deserialized.Address.State.ShouldBe("Greater London");
            deserialized.Address.Zip.ShouldBe("SW1A 1AA");
            deserialized.Address.Country.ShouldBe(CountryCode.GB);
        }

        [Fact]
        public void ShouldDeserializeNullableEmailOnIdentityDeclaredData()
        {
            const string json = @"{ ""name"": ""Hannah Bret"", ""email"": null }";

            var result = (IdentityDeclaredData)Serializer.Deserialize(json, typeof(IdentityDeclaredData));

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Hannah Bret");
            result.Email.ShouldBeNull();
        }

        // --------------------------------------------------------------------
        // Part E5 - phone_number on the IDV and FAV attempt request/response
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripPhoneNumberOnIdentityVerificationAttemptRequest()
        {
            var original = new IdentityVerificationAttemptRequest
            {
                RedirectUrl = "https://example.com/redirect",
                PhoneNumber = new PhoneNumber { CountryCode = "+33", Number = "5555550102" }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (IdentityVerificationAttemptRequest)
                Serializer.Deserialize(json, typeof(IdentityVerificationAttemptRequest));

            json.ShouldContain("\"phone_number\":");
            json.ShouldContain("\"country_code\":\"+33\"");
            json.ShouldContain("\"number\":\"5555550102\"");
            deserialized.PhoneNumber.CountryCode.ShouldBe("+33");
            deserialized.PhoneNumber.Number.ShouldBe("5555550102");
        }

        [Fact]
        public void ShouldRoundTripPhoneNumberOnFaceAuthenticationAttemptRequest()
        {
            var original = new FaceAuthenticationAttemptRequest
            {
                RedirectUrl = "https://example.com/redirect",
                PhoneNumber = new PhoneNumber { CountryCode = "+44", Number = "7700900000" }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (FaceAuthenticationAttemptRequest)
                Serializer.Deserialize(json, typeof(FaceAuthenticationAttemptRequest));

            json.ShouldContain("\"phone_number\":");
            deserialized.PhoneNumber.CountryCode.ShouldBe("+44");
            deserialized.PhoneNumber.Number.ShouldBe("7700900000");
        }

        [Fact]
        public void ShouldDeserializePhoneNumberOnIdentityVerificationAttemptResponse()
        {
            const string json = @"{ ""phone_number"": { ""country_code"": ""+33"", ""number"": ""5555550102"" } }";

            var result = (IdentityVerificationAttemptResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationAttemptResponse));

            result.ShouldNotBeNull();
            result.PhoneNumber.CountryCode.ShouldBe("+33");
            result.PhoneNumber.Number.ShouldBe("5555550102");
        }

        [Fact]
        public void ShouldDeserializePhoneNumberOnFaceAuthenticationAttemptResponse()
        {
            const string json = @"{ ""phone_number"": { ""country_code"": ""+44"", ""number"": ""7700900000"" } }";

            var result = (FaceAuthenticationAttemptResponse)
                Serializer.Deserialize(json, typeof(FaceAuthenticationAttemptResponse));

            result.ShouldNotBeNull();
            result.PhoneNumber.CountryCode.ShouldBe("+44");
            result.PhoneNumber.Number.ShouldBe("7700900000");
        }

        // --------------------------------------------------------------------
        // Part E6 - applicant_session_information gains number_of_sessions,
        // user_agent and initial_device
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripAllApplicantSessionInformationProperties()
        {
            var original = new ApplicantSessionInformation
            {
                IpAddress = "123.4.5.6",
                NumberOfSessions = 3,
                UserAgent = "Mozilla/5.0 (Linux; Android 8.0.0; SM-G960F Build/R16NW)",
                InitialDevice = Entities.InitialDevice.Mobile,
                SelectedDocuments = new List<SelectedDocument>
                {
                    new SelectedDocument { DocumentType = DocumentType.Passport }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ApplicantSessionInformation)
                Serializer.Deserialize(json, typeof(ApplicantSessionInformation));

            json.ShouldContain("\"number_of_sessions\":3");
            json.ShouldContain("\"user_agent\":");
            json.ShouldContain("\"initial_device\":\"mobile\"");
            deserialized.IpAddress.ShouldBe("123.4.5.6");
            deserialized.NumberOfSessions.ShouldBe(3);
            deserialized.UserAgent.ShouldStartWith("Mozilla/5.0");
            deserialized.InitialDevice.ShouldBe(Entities.InitialDevice.Mobile);
            deserialized.SelectedDocuments.Count.ShouldBe(1);
        }

        [Theory]
        [InlineData(Entities.InitialDevice.Desktop, "desktop")]
        [InlineData(Entities.InitialDevice.Mobile, "mobile")]
        public void ShouldSerializeEachInitialDeviceValue(InitialDevice device, string expected)
        {
            var session = new ApplicantSessionInformation { InitialDevice = device };

            var json = Serializer.Serialize(session);

            json.ShouldContain($"\"initial_device\":\"{expected}\"");
        }

        // --------------------------------------------------------------------
        // Part E7 - client_information forked. The identity verification
        // variant gains pre_selected_document_issuing_country and
        // pre_selected_document_type; the face authentication variant does not.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripAllIdentityVerificationClientInformationProperties()
        {
            var original = new IdentityVerificationClientInformation
            {
                PreSelectedResidenceCountry = CountryCode.FR,
                PreSelectedLanguage = "en-US",
                PreSelectedDocumentIssuingCountry = CountryCode.FR,
                PreSelectedDocumentType = DocumentType.Passport
            };

            var json = Serializer.Serialize(original);
            var deserialized = (IdentityVerificationClientInformation)
                Serializer.Deserialize(json, typeof(IdentityVerificationClientInformation));

            json.ShouldContain("\"pre_selected_document_issuing_country\":\"FR\"");
            json.ShouldContain("\"pre_selected_document_type\":\"Passport\"");
            deserialized.PreSelectedResidenceCountry.ShouldBe(CountryCode.FR);
            deserialized.PreSelectedLanguage.ShouldBe("en-US");
            deserialized.PreSelectedDocumentIssuingCountry.ShouldBe(CountryCode.FR);
            deserialized.PreSelectedDocumentType.ShouldBe(DocumentType.Passport);
        }

        [Fact]
        public void ShouldNotCarryIdentityOnlyFieldsOnTheFaceAuthenticationClientInformation()
        {
            var faceAuthenticationClientInformation = new ClientInformation
            {
                PreSelectedResidenceCountry = CountryCode.FR,
                PreSelectedLanguage = "en-US"
            };

            var json = Serializer.Serialize(faceAuthenticationClientInformation);

            json.ShouldNotContain("pre_selected_document_issuing_country");
            json.ShouldNotContain("pre_selected_document_type");
        }

        // --------------------------------------------------------------------
        // Part F M2 - document_type gained Other, Travel Document and Visa
        // --------------------------------------------------------------------

        [Theory]
        [InlineData(DocumentType.DrivingLicence, "Driving licence")]
        [InlineData(DocumentType.ID, "ID")]
        [InlineData(DocumentType.Other, "Other")]
        [InlineData(DocumentType.Passport, "Passport")]
        [InlineData(DocumentType.ResidencePermit, "Residence Permit")]
        [InlineData(DocumentType.TravelDocument, "Travel Document")]
        [InlineData(DocumentType.Visa, "Visa")]
        public void ShouldRoundTripEachDocumentTypeValue(DocumentType type, string expected)
        {
            var original = new SelectedDocument { DocumentType = type };

            var json = Serializer.Serialize(original);
            var deserialized = (SelectedDocument)Serializer.Deserialize(json, typeof(SelectedDocument));

            json.ShouldContain($"\"document_type\":\"{expected}\"");
            deserialized.DocumentType.ShouldBe(type);
        }

        // --------------------------------------------------------------------
        // Part F M3 / M4 - new status values
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripTheTerminatedAttemptStatus()
        {
            const string json = @"{ ""status"": ""terminated"" }";

            var result = (IdentityVerificationAttemptResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationAttemptResponse));

            result.ShouldNotBeNull();
            result.Status.ShouldBe(AttemptVerificationStatus.Terminated);
            Serializer.Serialize(result).ShouldContain("\"status\":\"terminated\"");
        }

        [Fact]
        public void ShouldRoundTripTheCreatedVerificationStatus()
        {
            const string json = @"{ ""status"": ""created"" }";

            var result = (IdentityVerificationResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationResponse));

            result.ShouldNotBeNull();
            result.Status.ShouldBe(IdentityVerificationStatus.Created);
            Serializer.Serialize(result).ShouldContain("\"status\":\"created\"");
        }

        // --------------------------------------------------------------------
        // Part F M5 - IdvDocument gained address and four permit fields
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripTheNewDocumentDetailsProperties()
        {
            var original = new DocumentDetails
            {
                DocumentType = DocumentType.ResidencePermit,
                DocumentIssuingCountry = CountryCode.GB,
                FrontImageSignedUrl = "https://example.com/front.png",
                Address = "123 Main Street, London, SW1A 1AA",
                PermitObtainingDate = "2020-01-15",
                PermitExpiryDate = "2030-01-14",
                PermitTypeDetailed = "Indefinite leave to remain",
                PermitTypeRemarks = "No work restrictions"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (DocumentDetails)Serializer.Deserialize(json, typeof(DocumentDetails));

            json.ShouldContain("\"address\":\"123 Main Street, London, SW1A 1AA\"");
            json.ShouldContain("\"permit_obtaining_date\":\"2020-01-15\"");
            json.ShouldContain("\"permit_expiry_date\":\"2030-01-14\"");
            json.ShouldContain("\"permit_type_detailed\":\"Indefinite leave to remain\"");
            json.ShouldContain("\"permit_type_remarks\":\"No work restrictions\"");
            deserialized.Address.ShouldBe("123 Main Street, London, SW1A 1AA");
            deserialized.PermitObtainingDate.ShouldBe("2020-01-15");
            deserialized.PermitExpiryDate.ShouldBe("2030-01-14");
            deserialized.PermitTypeDetailed.ShouldBe("Indefinite leave to remain");
            deserialized.PermitTypeRemarks.ShouldBe("No work restrictions");
            deserialized.DocumentType.ShouldBe(DocumentType.ResidencePermit);
        }

        // --------------------------------------------------------------------
        // The identity verification responses map the spec's "face" key, not
        // "face_image", which the default snake_case policy would produce.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeFaceFromTheSwaggerFaceKey()
        {
            const string json = @"{ ""face"": { ""image_signed_url"": ""https://example.com/face.png"" } }";

            var result = (IdentityVerificationResponse)
                Serializer.Deserialize(json, typeof(IdentityVerificationResponse));

            result.ShouldNotBeNull();
            result.FaceImage.ShouldNotBeNull();
            result.FaceImage.ImageSignedUrl.ShouldBe("https://example.com/face.png");
            Serializer.Serialize(result).ShouldContain("\"face\":");
        }
        // --------------------------------------------------------------------
        // Part F M1 - the attempts pagination query serializes to the exact
        // skip and limit query-string keys, and omits unset values.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAttemptsQueryToTheSwaggerQueryKeys()
        {
            var query = new AttemptsQuery { Skip = 6, Limit = 5 };

            var json = Serializer.Serialize(query);

            json.ShouldContain("\"skip\":6");
            json.ShouldContain("\"limit\":5");
        }

        [Fact]
        public void ShouldOmitUnsetAttemptsQueryValues()
        {
            var query = new AttemptsQuery { Limit = 10 };

            var json = Serializer.Serialize(query);

            json.ShouldContain("\"limit\":10");
            json.ShouldNotContain("skip");
        }

        [Fact]
        public void ShouldSerializeAttemptAssetsQueryToTheSwaggerQueryKeys()
        {
            var query = new AttemptAssetsQuery { Skip = 0, Limit = 10 };

            var json = Serializer.Serialize(query);

            json.ShouldContain("\"skip\":0");
            json.ShouldContain("\"limit\":10");
        }
        // --------------------------------------------------------------------
        // The identity verification REQUEST must be able to carry the full
        // declared-data shape. The spec's request schema declares
        // IdvIdentityDeclaredData, so phone_number, email and address are
        // sendable, not response-only.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeFullDeclaredDataOnIdentityVerificationRequest()
        {
            var request = new IdentityVerificationRequest
            {
                ApplicantId = "aplt_tkoi5db4hryu5cei5vwoabr7we",
                UserJourneyId = "usj_tkoi5db4hryu5cei5vwoabr7we",
                DeclaredData = new IdentityDeclaredData
                {
                    Name = "Hannah Bret",
                    BirthDate = "1994-10-15",
                    Email = "hannah.bret@example.com",
                    PhoneNumber = new PhoneNumber { CountryCode = "+33", Number = "5555550102" },
                    Address = new IdvAddress { AddressLine1 = "123 Main Street", City = "London", Country = CountryCode.GB }
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"birth_date\":\"1994-10-15\"");
            json.ShouldContain("\"email\":\"hannah.bret@example.com\"");
            json.ShouldContain("\"country_code\":\"+33\"");
            json.ShouldContain("\"address_line1\":\"123 Main Street\"");
        }

        [Fact]
        public void ShouldSerializeFullDeclaredDataOnIdentityVerificationAndAttemptRequest()
        {
            var request = new IdentityVerificationAndAttemptRequest
            {
                ApplicantId = "aplt_tkoi5db4hryu5cei5vwoabr7we",
                DeclaredData = new IdentityDeclaredData
                {
                    Name = "Hannah Bret",
                    PhoneNumber = new PhoneNumber { CountryCode = "+44", Number = "7700900000" }
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"phone_number\":");
            json.ShouldContain("\"number\":\"7700900000\"");
        }
        // --------------------------------------------------------------------
        // Country codes are typed (coding-standards: ISO 3166-1 alpha-2 uses
        // the CountryCode enum). CountryCode has no EnumMember attributes, so
        // it serializes as the bare ISO value. Nullable is required: the
        // tolerating converter returns the DEFAULT enum member for a
        // non-nullable target on an unknown value, which would silently read
        // an unrecognised country as "AF".
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeCountryCodesToBareIsoAlpha2Values()
        {
            var doc = new DocumentDetails
            {
                DocumentIssuingCountry = CountryCode.GB,
                Nationality = CountryCode.FR
            };

            var json = Serializer.Serialize(doc);

            json.ShouldContain("\"document_issuing_country\":\"GB\"");
            json.ShouldContain("\"nationality\":\"FR\"");
        }

        [Fact]
        public void ShouldRoundTripTypedCountryCodesAcrossTheIdentitiesArea()
        {
            var json = @"{
                ""document_issuing_country"": ""GB"",
                ""nationality"": ""FR""
            }";

            var doc = (DocumentDetails)Serializer.Deserialize(json, typeof(DocumentDetails));

            doc.DocumentIssuingCountry.ShouldBe(CountryCode.GB);
            doc.Nationality.ShouldBe(CountryCode.FR);
        }

        [Fact]
        public void ShouldReadAnUnknownCountryAsNullRatherThanTheDefaultEnumMember()
        {
            // "ZZ" is not an ISO alpha-2 code. Because the properties are nullable the
            // tolerating converter yields null; on a non-nullable property it would yield
            // the first enum member and silently misreport the country.
            const string json = @"{ ""nationality"": ""ZZ"" }";

            var doc = (DocumentDetails)Serializer.Deserialize(json, typeof(DocumentDetails));

            doc.ShouldNotBeNull();
            doc.Nationality.ShouldBeNull();
        }

        [Fact]
        public void ShouldKeepThePhonePrefixAsAStringNotACountryCode()
        {
            // IdvPhoneNumber.country_code is a phone prefix (pattern ^\+(\d+)$, e.g. +33),
            // not an ISO country code. It must stay a string.
            var phone = new PhoneNumber { CountryCode = "+33", Number = "5555550102" };

            var json = Serializer.Serialize(phone);

            json.ShouldContain("\"country_code\":\"+33\"");
        }

        // --------------------------------------------------------------------
        // risk_labels is declared per response class (Java's shape), not on the
        // shared base, so ID document verification does not silently inherit a
        // field its schema never returns.
        // --------------------------------------------------------------------

        [Fact]
        public void ShouldNotPopulateRiskLabelsOnIdDocumentVerification()
        {
            // The IDDV schema declares no risk_labels; the property is retained only for
            // backward compatibility and is scheduled for removal.
            const string json = @"{ ""status"": ""approved"" }";

            var result = (IdDocumentVerificationResponse)
                Serializer.Deserialize(json, typeof(IdDocumentVerificationResponse));

            result.ShouldNotBeNull();
            result.RiskLabels.ShouldBeNull();
        }
    }
}