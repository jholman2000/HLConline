Imports Microsoft.VisualBasic

Public Class hlc_Doctor
    Public ErrorMessage As String

    Public ID As Integer = 0
    Public FirstName As String = ""
    Public LastName As String = ""
    Public PracticeID As Integer = 0
    Public MobilePhone As String = ""
    Public HomePhone As String = ""
    Public Pager As String = ""
    Public EmailAddress As String = ""
    Public Attitude As AttitudeCode = AttitudeCode.Unknown
    Public FavAdultEmergency As Boolean = False
    Public FavAdultNonEmergency As Boolean = False
    Public NotFavAdult As Boolean = False
    Public FavChildEmergency As Boolean = False
    Public FavChildNonEmergency As Boolean = False
    Public NotFavChild As Boolean = False
    Public AcceptsMedicaid As Boolean = False
    Public ConsultAdultEmergency As Boolean = False
    Public ConsultChildEmergency As Boolean = False
    Public NOTES As String = ""
    Public DOCNOTES As String = ""
    Public DateLastUpdated As Date = Date.Today
    Public LastUpdatedBy As String = ""

    '-- New fields 1/30/11
    Public Status As StatusCode = StatusCode.NewlyIdentified
    Public StatusDate As Date = Date.Today
    Public RegContacted As YesNoUnknown = YesNoUnknown.Unknown
    Public SpecificallyKnown As YesNoUnknown = YesNoUnknown.Unknown
    Public FrequentlyTreat As YesNoUnknown = YesNoUnknown.Unknown
    Public Helpful As YesNoUnknown = YesNoUnknown.Unknown
    Public TreatYears As Integer = 0
    Public IsHRP As Boolean = False         ' High Risk Pregnancy
    Public IsBSMP As Boolean = False        ' Bloodless Surgery Management Program
    Public PeerReview As String = ""

    Public Practice As New hlc_Practice
    Public DoctorHospitals As New Collection    ' Of hlc_DoctorHospital
    Public DoctorSpecialties As New Collection  ' Of hlc_DoctorSpecialty
    Public DoctorNotes As Collection        ' Of hlc_DoctorNote

    Public Enum AttitudeCode As Integer
        Unknown = 0         '  NEED TO CONVERT 9 IN TABLE Attitude column TO 0 AND ADJUST ALL OTHERS 1->2  2->3 3->4 9->0
        Cooperative = 1
        Favorable = 2
        Limitations = 3
        NotFavorable = 4
    End Enum

    Public Enum StatusCode As Integer
        Unknown = 0
        NewlyIdentified = 1
        LetterSent = 2
        Deceased = 7
        MovedOutOfArea = 8
        Active = 9
        Retired = 10
    End Enum

    Public Enum YesNoUnknown As Integer
        Unknown = 0
        Yes = 1
        No = 2
    End Enum

    Private _BULLET As String = "&bull;&nbsp;"
    Private _CRLF As String = "<br>"


    Public Function GetDoctorByID(ByVal nID As Integer) As Boolean
        Dim hlcUtility As New hlc_Utility
        Dim oData As System.Data.DataRow

        Try
            oData = hlcUtility.GetDataRowViaSQL("select * from hlc_Doctor where ID = " + nID.ToString)

            If oData Is Nothing Then

                ErrorMessage = "Could not retrieve the information for Doctor ID " + nID.ToString + " " + _
                    hlcUtility.ErrorMessage
                Return False

            Else

                '-- Read the data into the public variables
                '
                ID = nID
                FirstName = oData.Item("FirstName").ToString
                LastName = oData.Item("LastName").ToString
                PracticeID = IIf(IsDBNull(oData.Item("PracticeID")), 0, oData.Item("PracticeID"))
                MobilePhone = oData.Item("MobilePhone").ToString
                HomePhone = oData.Item("HomePhone").ToString
                Pager = oData.Item("Pager").ToString
                EmailAddress = oData.Item("EmailAddress").ToString
                Attitude = oData.Item("Attitude")
                FavAdultEmergency = oData.Item("FavAdultEmergency")
                FavAdultNonEmergency = oData.Item("FavAdultNonEmergency")
                NotFavAdult = oData.Item("NotFavAdult")
                FavChildEmergency = oData.Item("FavChildEmergency")
                FavChildNonEmergency = oData.Item("FavChildNonEmergency")
                NotFavChild = oData.Item("NotFavChild")
                AcceptsMedicaid = oData.Item("AcceptsMedicaid")
                ConsultAdultEmergency = oData.Item("ConsultAdultEmergency")
                ConsultChildEmergency = oData.Item("ConsultChildEmergency")
                NOTES = oData.Item("NOTES").ToString
                DOCNOTES = oData.Item("DOCNOTES").ToString
                DateLastUpdated = IIf(IsDBNull(oData.Item("DateLastUpdated")), Date.MinValue, oData.Item("DateLastUpdated"))
                LastUpdatedBy = oData.Item("LastUpdatedBy").ToString
                IsBSMP = oData.Item("IsBSMP")
                RegContacted = oData.Item("RegContacted")
                SpecificallyKnown = oData.Item("SpecificallyKnown")
                Helpful = oData.Item("Helpful")
                FrequentlyTreat = oData.Item("FrequentlyTreat")
                TreatYears = oData.Item("TreatYears")
                Status = oData.Item("Status")
                StatusDate = IIf(IsDBNull(oData.Item("StatusDate")), Date.Today, oData.Item("StatusDate"))
                IsHRP = oData.Item("IsHRP")
                PeerReview = oData.Item("PeerReview").ToString

                If PracticeID > 0 Then
                    Practice.GetPracticeByID(PracticeID)
                End If

                GetHospitals()
                GetSpecialties()

            End If

            Return True

        Catch ex As Exception

            ErrorMessage = ex.Message
            Return False

        End Try

    End Function

    Public Function GetHospitals() As Boolean
        '-- Read a list of Hospitals for this Doctor into the Hospitals collection
        '   (Assumes the ID has already been set)
        '
        Dim hlcUtility As New hlc_Utility
        Dim oTable As System.Data.DataTable
        Dim oDataRow As System.Data.DataRow
        Dim hlcDoctorHospital As hlc_DoctorHospital

        'DoctorHospitals = Nothing

        oTable = hlcUtility.GetDataTableViaSQL("exec hlc_GetDoctorHospitals " + ID.ToString)

        If Not oTable Is Nothing Then

            DoctorHospitals = New Collection
            For Each oDataRow In oTable.Rows
                hlcDoctorHospital = New hlc_DoctorHospital
                With hlcDoctorHospital
                    .ID = oDataRow.Item("ID")
                    .DoctorID = oDataRow.Item("DoctorID")
                    .HospitalID = oDataRow.Item("HospitalID")
                    .HospitalName = oDataRow.Item("HospitalName").ToString
                    .City = oDataRow.Item("City").ToString
                    .State = oDataRow.Item("State").ToString
                    .Notes = oDataRow.Item("Notes").ToString
                    .CommitteeID = oDataRow.Item("CommitteeID").ToString
                End With
                DoctorHospitals.Add(hlcDoctorHospital)
            Next
            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetHospitals(ByVal nID As Integer) As Boolean
        ID = nID
        GetHospitals = GetHospitals()
    End Function

    Public Function GetSpecialties() As Boolean
        '-- Read a list of specialties from the hlc_DoctorSpecialty table and places
        '   them into the Specialties collection
        '   (Assumes the ID has already been set)
        '
        Dim hlcUtility As New hlc_Utility
        Dim oTable As System.Data.DataTable
        Dim oDataRow As System.Data.DataRow
        Dim hlcDoctorSpecialty As hlc_DoctorSpecialty

        'DoctorSpecialties = Nothing

        oTable = hlcUtility.GetDataTableViaSQL("exec hlc_GetDoctorSpecialties " + ID.ToString)

        If Not oTable Is Nothing Then

            DoctorSpecialties = New Collection
            For Each oDataRow In oTable.Rows
                hlcDoctorSpecialty = New hlc_DoctorSpecialty
                With hlcDoctorSpecialty
                    .ID = oDataRow.Item("ID")
                    .DoctorID = oDataRow.Item("DoctorID")
                    .SpecialtyID = oDataRow.Item("SpecialtyID")
                    .SpecialtyName = oDataRow.Item("SpecialtyName").ToString
                    .SpecialtyCode = oDataRow.Item("SpecialtyCode").ToString
                    .AreaOfExpertise = oDataRow.Item("AreaOfExpertise").ToString
                End With
                DoctorSpecialties.Add(hlcDoctorSpecialty)
            Next

        End If

        Return True

    End Function

    Public Function GetSpecialties(ByVal nID As Integer) As Boolean
        ID = nID
        GetSpecialties = GetSpecialties()
    End Function

    Public Function GetNotes() As Boolean
        '-- Read a list of notes from the hlc_DoctorNote table and places
        '   them into the DoctorNotes collection
        '   (Assumes the ID has already been set)
        '
        Dim hlcUtility As New hlc_Utility
        Dim oTable As System.Data.DataTable
        Dim oDataRow As System.Data.DataRow
        Dim hlcDoctorNote As hlc_DoctorNote

        DoctorNotes = Nothing

        oTable = hlcUtility.GetDataTableViaSQL("exec hlc_GetDoctorNotes " + ID.ToString)

        If Not oTable Is Nothing Then

            DoctorNotes = New Collection
            For Each oDataRow In oTable.Rows
                hlcDoctorNote = New hlc_DoctorNote
                With hlcDoctorNote
                    .ID = oDataRow.Item("ID")
                    .DoctorID = oDataRow.Item("DoctorID")
                    .UserID = oDataRow.Item("UserID")
                    .FullName = oDataRow.Item("FullName").ToString
                    .Notes = oDataRow.Item("Notes").ToString
                    .DateEntered = oDataRow.Item("DateEntered")
                    .FileName = oDataRow.Item("FileName").ToString
                End With
                DoctorNotes.Add(hlcDoctorNote)
            Next

        End If

        Return True

    End Function

    Public Function GetNotes(ByVal nID As Integer) As Boolean
        ID = nID
        GetNotes = GetNotes()
    End Function

    Public Function AddNew(ByVal CurrentUserID As String, ByVal DocHospIDs As String, ByVal DocHospNotes As String, ByVal DocSpecIDs As String, ByVal DocSpecNotes As String) As Boolean
        '
        ' Add a new hlc_Doctor record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who added the record.
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        'Dim oParm As Data.SqlClient.SqlParameter
        Dim aDocHospIDs() As String
        Dim aDocHospNotes() As String
        Dim aDocSpecIDs() As String
        Dim aDocSpecNotes() As String
        Dim intRecsAffected As Integer = 0

        ErrorMessage = ""

        '===================================================================
        ' CALL STORED PROC TO ADD RECORD
        '===================================================================
        If ErrorMessage = "" Then

            Try

                oConn.ConnectionString = GetConnectionString()
                oConn.Open()

                oCmd.Connection = oConn
                oCmd.CommandText = "hlc_InsertDoctor"
                oCmd.CommandType = Data.CommandType.StoredProcedure
                'oParm = oCmd.Parameters.AddWithValue("@ID", ID)
                'oParm.Direction = ParameterDirection.Output
                oCmd.Parameters.AddWithValue("@FirstName", FirstName)
                oCmd.Parameters.AddWithValue("@LastName", LastName)
                oCmd.Parameters.AddWithValue("@MobilePhone", MobilePhone)
                oCmd.Parameters.AddWithValue("@HomePhone", HomePhone)
                oCmd.Parameters.AddWithValue("@Pager", Pager)
                oCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
                oCmd.Parameters.AddWithValue("@Attitude", Attitude)
                oCmd.Parameters.AddWithValue("@FavAdultEmergency", FavAdultEmergency)
                oCmd.Parameters.AddWithValue("@FavAdultNonEmergency", FavAdultNonEmergency)
                oCmd.Parameters.AddWithValue("@NotFavAdult", NotFavAdult)
                oCmd.Parameters.AddWithValue("@FavChildEmergency", FavChildEmergency)
                oCmd.Parameters.AddWithValue("@FavChildNonEmergency", FavChildNonEmergency)
                oCmd.Parameters.AddWithValue("@NotFavChild", NotFavChild)
                oCmd.Parameters.AddWithValue("@AcceptsMedicaid", AcceptsMedicaid)
                oCmd.Parameters.AddWithValue("@ConsultAdultEmergency", ConsultAdultEmergency)
                oCmd.Parameters.AddWithValue("@ConsultChildEmergency", ConsultChildEmergency)
                oCmd.Parameters.AddWithValue("@DateLastUpdated", Today)
                oCmd.Parameters.AddWithValue("@LastUpdatedBy", CurrentUserID)
                oCmd.Parameters.AddWithValue("@PeerReview", PeerReview)
                oCmd.Parameters.AddWithValue("@IsHRP", IsHRP)
                oCmd.Parameters.AddWithValue("@IsBSMP", IsBSMP)
                oCmd.Parameters.AddWithValue("@RegContacted", RegContacted)
                oCmd.Parameters.AddWithValue("@SpecificallyKnown", SpecificallyKnown)
                oCmd.Parameters.AddWithValue("@Helpful", Helpful)
                oCmd.Parameters.AddWithValue("@FrequentlyTreat", FrequentlyTreat)
                oCmd.Parameters.AddWithValue("@TreatYears", TreatYears)
                oCmd.Parameters.AddWithValue("@Status", Status)
                oCmd.Parameters.AddWithValue("@StatusDate", StatusDate)

                '-- Pass in all Practice fields.  If PracticeID = -1 then the remaining fields will be used
                '   to Insert a new Practice
                oCmd.Parameters.AddWithValue("@PracticeID", PracticeID)
                oCmd.Parameters.AddWithValue("@PracticeName", Practice.PracticeName)
                oCmd.Parameters.AddWithValue("@Address1", Practice.Address1)
                oCmd.Parameters.AddWithValue("@Address2", Practice.Address2)
                oCmd.Parameters.AddWithValue("@City", Practice.City)
                oCmd.Parameters.AddWithValue("@State", Practice.State)
                oCmd.Parameters.AddWithValue("@Zip", Practice.Zip)
                oCmd.Parameters.AddWithValue("@OfficePhone1", Practice.OfficePhone1)
                oCmd.Parameters.AddWithValue("@OfficePhone2", Practice.OfficePhone2)
                oCmd.Parameters.AddWithValue("@Fax", Practice.Fax)

                '-- DocHospIDs and other similar params have 6 values separated by a pipe (|).  Unused IDs will be 0 or blank.
                aDocHospIDs = Split(DocHospIDs, "|")
                aDocHospNotes = Split(DocHospNotes, "|")
                oCmd.Parameters.AddWithValue("@HospID1", aDocHospIDs(0))
                oCmd.Parameters.AddWithValue("@HospID2", aDocHospIDs(1))
                oCmd.Parameters.AddWithValue("@HospID3", aDocHospIDs(2))
                oCmd.Parameters.AddWithValue("@HospID4", aDocHospIDs(3))
                oCmd.Parameters.AddWithValue("@HospID5", aDocHospIDs(4))
                oCmd.Parameters.AddWithValue("@HospID6", aDocHospIDs(5))
                oCmd.Parameters.AddWithValue("@HospNotes1", aDocHospNotes(0))
                oCmd.Parameters.AddWithValue("@HospNotes2", aDocHospNotes(1))
                oCmd.Parameters.AddWithValue("@HospNotes3", aDocHospNotes(2))
                oCmd.Parameters.AddWithValue("@HospNotes4", aDocHospNotes(3))
                oCmd.Parameters.AddWithValue("@HospNotes5", aDocHospNotes(4))
                oCmd.Parameters.AddWithValue("@HospNotes6", aDocHospNotes(5))

                aDocSpecIDs = Split(DocSpecIDs, "|")
                aDocSpecNotes = Split(DocSpecNotes, "|")
                oCmd.Parameters.AddWithValue("@SpecID1", aDocSpecIDs(0))
                oCmd.Parameters.AddWithValue("@SpecID2", aDocSpecIDs(1))
                oCmd.Parameters.AddWithValue("@SpecID3", aDocSpecIDs(2))
                oCmd.Parameters.AddWithValue("@SpecID4", aDocSpecIDs(3))
                oCmd.Parameters.AddWithValue("@SpecID5", aDocSpecIDs(4))
                oCmd.Parameters.AddWithValue("@SpecID6", aDocSpecIDs(5))
                oCmd.Parameters.AddWithValue("@SpecArea1", aDocSpecNotes(0))
                oCmd.Parameters.AddWithValue("@SpecArea2", aDocSpecNotes(1))
                oCmd.Parameters.AddWithValue("@SpecArea3", aDocSpecNotes(2))
                oCmd.Parameters.AddWithValue("@SpecArea4", aDocSpecNotes(3))
                oCmd.Parameters.AddWithValue("@SpecArea5", aDocSpecNotes(4))
                oCmd.Parameters.AddWithValue("@SpecArea6", aDocSpecNotes(5))

                Dim oOut As New Data.SqlClient.SqlParameter("@ID", Data.SqlDbType.Int)
                oOut.Direction = Data.ParameterDirection.Output
                oCmd.Parameters.Add(oOut)

                intRecsAffected = oCmd.ExecuteNonQuery()

                ID = oCmd.Parameters("@ID").Value

            Catch ex As Exception

                ErrorMessage = ex.Message
                intRecsAffected = -1
                Return False

            Finally

                '-- Close up shop before leaving
                If oConn.State = Data.ConnectionState.Open Then
                    oConn.Close()
                End If

            End Try

        End If

        Return True

    End Function

    Public Function Update(ByVal CurrentUserID As String, ByVal DocHospIDs As String, ByVal DocHospNotes As String, ByVal DocSpecIDs As String, ByVal DocSpecNotes As String) As Boolean
        '
        ' Update an existing hlc_Doctor record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        Dim aDocHospIDs() As String
        Dim aDocHospNotes() As String
        Dim aDocSpecIDs() As String
        Dim aDocSpecNotes() As String
        Dim intRecsAffected As Integer = 0

        ErrorMessage = ""

        '===================================================================
        ' CALL STORED PROC TO ADD RECORD
        '===================================================================
        If ErrorMessage = "" Then

            Try

                oConn.ConnectionString = GetConnectionString()
                oConn.Open()

                oCmd.Connection = oConn
                oCmd.CommandText = "hlc_UpdateDoctor"
                oCmd.CommandType = Data.CommandType.StoredProcedure
                oCmd.Parameters.AddWithValue("@ID", ID)
                oCmd.Parameters.AddWithValue("@FirstName", FirstName)
                oCmd.Parameters.AddWithValue("@LastName", LastName)
                oCmd.Parameters.AddWithValue("@MobilePhone", MobilePhone)
                oCmd.Parameters.AddWithValue("@HomePhone", HomePhone)
                oCmd.Parameters.AddWithValue("@Pager", Pager)
                oCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
                oCmd.Parameters.AddWithValue("@Attitude", Attitude)
                oCmd.Parameters.AddWithValue("@FavAdultEmergency", FavAdultEmergency)
                oCmd.Parameters.AddWithValue("@FavAdultNonEmergency", FavAdultNonEmergency)
                oCmd.Parameters.AddWithValue("@NotFavAdult", NotFavAdult)
                oCmd.Parameters.AddWithValue("@FavChildEmergency", FavChildEmergency)
                oCmd.Parameters.AddWithValue("@FavChildNonEmergency", FavChildNonEmergency)
                oCmd.Parameters.AddWithValue("@NotFavChild", NotFavChild)
                oCmd.Parameters.AddWithValue("@AcceptsMedicaid", AcceptsMedicaid)
                oCmd.Parameters.AddWithValue("@ConsultAdultEmergency", ConsultAdultEmergency)
                oCmd.Parameters.AddWithValue("@ConsultChildEmergency", ConsultChildEmergency)
                oCmd.Parameters.AddWithValue("@DateLastUpdated", Today)
                oCmd.Parameters.AddWithValue("@LastUpdatedBy", CurrentUserID)
                oCmd.Parameters.AddWithValue("@PeerReview", PeerReview)
                oCmd.Parameters.AddWithValue("@IsHRP", IsHRP)
                oCmd.Parameters.AddWithValue("@IsBSMP", IsBSMP)
                oCmd.Parameters.AddWithValue("@RegContacted", RegContacted)
                oCmd.Parameters.AddWithValue("@SpecificallyKnown", SpecificallyKnown)
                oCmd.Parameters.AddWithValue("@Helpful", Helpful)
                oCmd.Parameters.AddWithValue("@FrequentlyTreat", FrequentlyTreat)
                oCmd.Parameters.AddWithValue("@TreatYears", TreatYears)
                oCmd.Parameters.AddWithValue("@Status", Status)
                oCmd.Parameters.AddWithValue("@StatusDate", StatusDate)

                '-- Pass in all Practice fields.  If PracticeID = -1 then the remaining fields will be used
                '   to Insert a new Practice
                oCmd.Parameters.AddWithValue("@PracticeID", PracticeID)
                oCmd.Parameters.AddWithValue("@PracticeName", Practice.PracticeName)
                oCmd.Parameters.AddWithValue("@Address1", Practice.Address1)
                oCmd.Parameters.AddWithValue("@Address2", Practice.Address2)
                oCmd.Parameters.AddWithValue("@City", Practice.City)
                oCmd.Parameters.AddWithValue("@State", Practice.State)
                oCmd.Parameters.AddWithValue("@Zip", Practice.Zip)
                oCmd.Parameters.AddWithValue("@OfficePhone1", Practice.OfficePhone1)
                oCmd.Parameters.AddWithValue("@OfficePhone2", Practice.OfficePhone2)
                oCmd.Parameters.AddWithValue("@Fax", Practice.Fax)

                '-- DocHospIDs and other similar params have 6 values separated by a pipe (|).  Unused IDs will be 0.
                aDocHospIDs = Split(DocHospIDs, "|")
                aDocHospNotes = Split(DocHospNotes, "|")
                oCmd.Parameters.AddWithValue("@HospID1", aDocHospIDs(0))
                oCmd.Parameters.AddWithValue("@HospID2", aDocHospIDs(1))
                oCmd.Parameters.AddWithValue("@HospID3", aDocHospIDs(2))
                oCmd.Parameters.AddWithValue("@HospID4", aDocHospIDs(3))
                oCmd.Parameters.AddWithValue("@HospID5", aDocHospIDs(4))
                oCmd.Parameters.AddWithValue("@HospID6", aDocHospIDs(5))
                oCmd.Parameters.AddWithValue("@HospNotes1", aDocHospNotes(0))
                oCmd.Parameters.AddWithValue("@HospNotes2", aDocHospNotes(1))
                oCmd.Parameters.AddWithValue("@HospNotes3", aDocHospNotes(2))
                oCmd.Parameters.AddWithValue("@HospNotes4", aDocHospNotes(3))
                oCmd.Parameters.AddWithValue("@HospNotes5", aDocHospNotes(4))
                oCmd.Parameters.AddWithValue("@HospNotes6", aDocHospNotes(5))

                aDocSpecIDs = Split(DocSpecIDs, "|")
                aDocSpecNotes = Split(DocSpecNotes, "|")
                oCmd.Parameters.AddWithValue("@SpecID1", aDocSpecIDs(0))
                oCmd.Parameters.AddWithValue("@SpecID2", aDocSpecIDs(1))
                oCmd.Parameters.AddWithValue("@SpecID3", aDocSpecIDs(2))
                oCmd.Parameters.AddWithValue("@SpecID4", aDocSpecIDs(3))
                oCmd.Parameters.AddWithValue("@SpecID5", aDocSpecIDs(4))
                oCmd.Parameters.AddWithValue("@SpecID6", aDocSpecIDs(5))
                oCmd.Parameters.AddWithValue("@SpecArea1", aDocSpecNotes(0))
                oCmd.Parameters.AddWithValue("@SpecArea2", aDocSpecNotes(1))
                oCmd.Parameters.AddWithValue("@SpecArea3", aDocSpecNotes(2))
                oCmd.Parameters.AddWithValue("@SpecArea4", aDocSpecNotes(3))
                oCmd.Parameters.AddWithValue("@SpecArea5", aDocSpecNotes(4))
                oCmd.Parameters.AddWithValue("@SpecArea6", aDocSpecNotes(5))


                intRecsAffected = oCmd.ExecuteNonQuery()

            Catch ex As Exception

                ErrorMessage = ex.Message
                intRecsAffected = -1
                Return False

            Finally

                '-- Close up shop before leaving
                If oConn.State = Data.ConnectionState.Open Then
                    oConn.Close()
                End If

            End Try

        End If

        Return True
    End Function

    Public Function Delete(ByVal CurrentUserID As String) As Boolean
        '
        ' Delete an existing hlc_Doctor record using the ID in the Public variable.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who deleted the record.
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        'Dim hlcUtility As hlc_Utility
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        '===================================================================
        ' CALL STORED PROC TO DELETE THIS RECORD
        '===================================================================
        Try
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_DeleteDoctor"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)

            intRecsAffected = oCmd.ExecuteNonQuery()
            If intRecsAffected > 0 Then
                Return True
            End If

        Catch ex As Exception

            ErrorMessage = ex.Message
            intRecsAffected = -1
            Return False

        Finally

            '-- Close up shop before leaving
            If oConn.State = Data.ConnectionState.Open Then
                oConn.Close()
            End If

        End Try

        Return True

    End Function

    Private Function YesNoUnknownText(ByVal theValue As YesNoUnknown) As String
        Dim sTemp As String = ""
        Select Case theValue
            Case YesNoUnknown.No
                sTemp = "No"
            Case YesNoUnknown.Yes
                sTemp = "Yes"
            Case YesNoUnknown.Unknown
                sTemp = "Unknown"
        End Select
        Return sTemp
    End Function

    Public ReadOnly Property AttitudeText(Optional ByVal TerseText As Boolean = False) As String
        '-- Returns a more readable version of the Attitude selections
        '
        Get
            Dim sTemp As String = ""
            sTemp = _BULLET + "Is Doctor regularly contacted? " + YesNoUnknownText(RegContacted) + _CRLF
            sTemp += _BULLET + "Is Doctor specifically known by you? " + YesNoUnknownText(SpecificallyKnown) + _CRLF
            sTemp += _BULLET + "Was Doctor helpful in specific cases? " + YesNoUnknownText(Helpful) + _CRLF
            sTemp += _BULLET + "Does Doctor frequently treat witnesses? " + YesNoUnknownText(FrequentlyTreat)
            If FrequentlyTreat = YesNoUnknown.Yes Then sTemp += " (" + TreatYears.ToString + " years)"
            sTemp += _CRLF

            If FavAdultEmergency And FavAdultNonEmergency Then
                sTemp += _BULLET + "Favorable for Adult Emergency and Non-Emergency" + _CRLF
            End If
            If FavAdultEmergency And Not FavAdultNonEmergency Then
                sTemp += _BULLET + "Favorable Adult Emergency only" + _CRLF
            End If
            If Not FavAdultEmergency And FavAdultNonEmergency Then
                sTemp += _BULLET + "Favorable Adult Non-Emergency only" + _CRLF
            End If
            '-- CHILD
            If FavChildEmergency And FavChildNonEmergency Then
                sTemp += _BULLET + "Favorable for Child Emergency and Non-Emergency" + _CRLF
            End If
            If FavChildEmergency And Not FavChildNonEmergency Then
                sTemp += _BULLET + "Favorable Child Emergency only" + _CRLF
            End If
            If Not FavChildEmergency And FavChildNonEmergency Then
                sTemp += _BULLET + "Favorable Child Non-Emergency only" + _CRLF
            End If
            If NotFavAdult And NotFavChild Then
                sTemp += _BULLET + "NOT Favorable for Adult OR Child Emergency" + _CRLF
            Else
                If NotFavAdult Then
                    sTemp += _BULLET + "NOT Favorable for Adult Emergency" + _CRLF
                End If
                If NotFavChild Then
                    sTemp += _BULLET + "NOT Favorable for Child Emergency" + _CRLF
                End If
            End If
            If ConsultAdultEmergency And ConsultChildEmergency Then
                sTemp += _BULLET + "Will consult for Adult and Child" + _CRLF
            Else
                If ConsultAdultEmergency Then
                    sTemp += _BULLET + "Will consult for Adult" + _CRLF
                End If
                If ConsultChildEmergency Then
                    sTemp += _BULLET + "Will consult for Child" + _CRLF
                End If
            End If
            If AcceptsMedicaid Then
                sTemp += _BULLET + "Accepts Medicaid"
            End If

            AttitudeText = sTemp
        End Get
    End Property

    Public ReadOnly Property HospitalsText() As String
        '
        ' Returns a more readable version of the list of Hospitals for this
        ' Doctor
        Get
            Dim sTemp As String = ""
            Dim hlcDoctorHospital As hlc_DoctorHospital

            For Each hlcDoctorHospital In DoctorHospitals
                sTemp += _BULLET + hlcDoctorHospital.HospitalName
                If hlcDoctorHospital.Notes <> "" Then
                    sTemp += ": " + hlcDoctorHospital.Notes
                End If
                sTemp += _CRLF
            Next

            HospitalsText = sTemp

        End Get
    End Property

    Public ReadOnly Property SpecialtiesText() As String
        '
        ' Returns a more readable version of the list of Specialties for this
        ' Doctor
        Get
            Dim sTemp As String = ""
            Dim hlcDoctorSpecialty As hlc_DoctorSpecialty

            For Each hlcDoctorSpecialty In DoctorSpecialties
                sTemp += _BULLET + hlcDoctorSpecialty.SpecialtyName
                If hlcDoctorSpecialty.AreaOfExpertise <> "" Then
                    sTemp += ": " + hlcDoctorSpecialty.AreaOfExpertise
                End If
                sTemp += _CRLF
            Next

            SpecialtiesText = sTemp
        End Get
    End Property

    Public ReadOnly Property StatusText() As String
        '
        ' Returns a readable version of the Status
        '
        Get
            Dim sTemp As String = ""
            Select Case Status
                Case StatusCode.Active
                    sTemp = "Active"
                Case StatusCode.Deceased
                    sTemp = "Deceased"
                Case StatusCode.LetterSent
                    sTemp = "Intro letter sent"
                Case StatusCode.MovedOutOfArea
                    sTemp = "Moved out of area"
                Case StatusCode.NewlyIdentified
                    sTemp = "Newly identified Doctor"
                Case StatusCode.Retired
                    sTemp = "Retired"
                Case StatusCode.Unknown
                    sTemp = "Unknown"
            End Select

            StatusText = sTemp

        End Get
    End Property

    Public ReadOnly Property FullName() As String
        Get
            FullName = RTrim(FirstName) + " " + LastName
        End Get
    End Property

    Private Function GetConnectionString() As String
        Dim hlcUtility As New hlc_Utility
        Return hlcUtility.GetConnectionString()
    End Function
End Class
