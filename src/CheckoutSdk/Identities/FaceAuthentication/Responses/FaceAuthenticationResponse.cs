using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Identities.FaceAuthentication.Responses
{
    public class FaceAuthenticationResponse : Resource
    {
        /// <summary>
        /// The face authentication's unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Your configuration ID
        /// </summary>
        public string UserJourneyId { get; set; }

        /// <summary>
        /// The applicant's unique identifier
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// The face authentication status
        /// </summary>
        public FaceAuthenticationStatus Status { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// </summary>
        public List<ResponseCode> ResponseCodes { get; set; }

        /// <summary>
        /// One or more codes that provide more information about risks associated with the verification
        /// </summary>
        public List<string> RiskLabels { get; set; }

        /// <summary>
        /// The details of the image of the applicant's face extracted from the video
        /// </summary>
        public FaceDetails Face { get; set; }
    }

    public class FaceAuthenticationAttemptResponse : Resource
    {
        /// <summary>
        /// The unique identifier for the face authentication attempt
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The URL to redirect the applicant to after the attempt
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The attempt status
        /// </summary>
        public FaceAuthenticationAttemptStatus Status { get; set; }

        /// <summary>
        /// The date and time when the resource was created, in UTC
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The date and time when the resource was modified, in UTC
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// One or more response codes that provide more information about the status
        /// </summary>
        public List<ResponseCode> ResponseCodes { get; set; }

        /// <summary>
        /// The applicant's details
        /// </summary>
        public ClientInformation ClientInformation { get; set; }

        /// <summary>
        /// The details of the attempt
        /// </summary>
        public ApplicantSessionInformation ApplicantSessionInformation { get; set; }

        /// <summary>
        /// The verification URL from the links
        /// </summary>
        public string VerificationUrl { get; set; }
    }

    public class FaceAuthenticationAttemptsResponse : Resource
    {
        /// <summary>
        /// The total number of attempts
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The number of attempts skipped
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The maximum number of attempts returned
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The list of attempts
        /// </summary>
        public List<FaceAuthenticationAttemptResponse> Data { get; set; }
    }

    public class ResponseCode
    {
        /// <summary>
        /// The response code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The description of the response code
        /// </summary>
        public string Summary { get; set; }
    }

    public class FaceDetails
    {
        /// <summary>
        /// The URL to the face image
        /// </summary>
        public string ImageSignedUrl { get; set; }
    }

    public class ApplicantSessionInformation
    {
        /// <summary>
        /// The applicant's IP address during the attempt
        /// </summary>
        public string IpAddress { get; set; }
    }

    public class ClientInformation
    {
        /// <summary>
        /// The applicant's residence country (ISO alpha-2 country code)
        /// </summary>
        public string PreSelectedResidenceCountry { get; set; }

        /// <summary>
        /// The language for the user interface (IETF BCP 47 language tag)
        /// </summary>
        public string PreSelectedLanguage { get; set; }
    }

    public enum FaceAuthenticationStatus
    {
        Approved,
        CaptureInProgress,
        ChecksInProgress,
        Created,
        Declined,
        Inconclusive,
        Pending,
        Refused,
        RetryRequired
    }

    public enum FaceAuthenticationAttemptStatus
    {
        CaptureAborted,
        CaptureInProgress,
        ChecksInconclusive,
        ChecksInProgress,
        Completed,
        Expired,
        PendingRedirection,
        CaptureRefused
    }
}