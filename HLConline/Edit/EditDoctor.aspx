<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="EditDoctor.aspx.vb" Inherits="HLConline.EditDoctor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td>
    <asp:Label ID="lblHeading" runat="server" CssClass="ContentHeading" Text="Add a New Doctor"></asp:Label></td>
            <td align="right">
                <asp:Button ID="btnDelete" runat="server" CssClass="ContentButton" Text="Delete" Visible="False" />&nbsp;&nbsp;
                <asp:Button ID="btnUpdate" runat="server" CssClass="ContentButton" 
                    Text="Update" onclick="btnUpdate_Click" /></td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlError" runat="server" Height="50px" Width="580px" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" Visible="False" BackColor="Cornsilk">
        <table cellpadding="3"><tr><td valign="top">
        <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/images/error_big.gif" />&nbsp;<strong><br />
        </strong>
        </td>
            <td valign="top">
                Unable to update this User because of the following:<br />
        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></td>
        </tr></table>
    </asp:Panel>
    <asp:Panel ID="pnlMessage" runat="server" Width="580px" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" Visible="False" BackColor="Cornsilk">
        <table cellpadding="3">
        <tr>
        <td></td>
        <td valign="top" align="center"><asp:Label ID="lblMessage" runat="server" ForeColor="Black" EnableViewState="False" CssClass="ContentText"></asp:Label></td>
        </tr></table>
    </asp:Panel>
    <br />
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td valign="top" width="40%">
    <%-- BEGIN Doctor name and address information  --%>
    <table style="width: 100%" cellpadding="1" cellspacing="0">
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label1" runat="server" CssClass="LabelDataEntry" Text="Name:" EnableViewState="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="110px" TabIndex="1"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="110px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Mobile Phone:" EnableViewState="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="TextBoxDataEntry" MaxLength="12" Width="100px" TabIndex="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Pager:" EnableViewState="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPager" runat="server" CssClass="TextBoxDataEntry" MaxLength="12" Width="100px" TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Home Phone:" EnableViewState="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtHomePhone" runat="server" CssClass="TextBoxDataEntry" MaxLength="12" Width="100px" TabIndex="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label2" runat="server" CssClass="LabelDataEntry" Text="Email:" EnableViewState="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TextBoxDataEntry" MaxLength="80" Width="220px" TabIndex="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 80px" valign="top"><asp:Label ID="Label3" runat="server" CssClass="LabelDataEntry" Text="Status:" EnableViewState="False" Font-Bold=true></asp:Label></td>
            <td>
              <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDownDataEntry" AutoPostBack="false" TabIndex="7" Width="220px">
                  <asp:ListItem Value="0" Selected="True">Unknown</asp:ListItem>
                  <asp:ListItem Value="1">Newly identified Doctor</asp:ListItem>
                  <asp:ListItem Value="2">Intro letter sent; Pending reply</asp:ListItem>
                  <asp:ListItem Value="7">Deceased</asp:ListItem>
                  <asp:ListItem Value="8">Moved out of the area</asp:ListItem>
                  <asp:ListItem Value="9">Active</asp:ListItem>
                  <asp:ListItem Value="10">Retired</asp:ListItem>
              </asp:DropDownList><br />
              <asp:HiddenField runat="server" ID="hidOriginalStatus" Value="" />
              <asp:HiddenField runat="server" ID="hidStatusDate" Value="" />
          </td>
        </tr>
    </table>
    <%-- END Doctor name and address information  --%>
    </td>
    <td width="2%">&nbsp;</td>
    <td valign="top" width="58%">
    <%-- BEGIN Practice dropdown and data entry  --%>
    <table style="width: 100%" cellpadding="1" cellspacing="1">
      <tr>
        <td valign="top" style="width: 51px">
          <asp:Label ID="Label7" runat="server" CssClass="LabelDataEntry" Text="Practice:" EnableViewState="False"></asp:Label>
        </td>
        <td valign="top">
          <asp:DropDownList ID="ddlPractice" runat="server" CssClass="DropDownDataEntry" 
                AutoPostBack="True" TabIndex="8" AppendDataBoundItems="true" DataSourceID="sdsPractice" 
                DataValueField="ID" DataTextField="PracticeName" Width="400px">
            <asp:ListItem Value="0" Selected="True">(Select a Practice)</asp:ListItem>
          </asp:DropDownList><br />&nbsp;
          <br />
          <asp:Label ID="lblPracticeAddress" runat="server" CssClass="ContentText" EnableViewState="True" Visible="false"></asp:Label>
        </td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>
          <asp:Panel runat="server" ID="pnlPractice" Visible="false">
          <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Practice Name:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticeName" runat="server" CssClass="TextBoxDataEntry" MaxLength="80" Width="200px" TabIndex="9"/>
              </td>
            </tr>
            <tr>
              <td style="width: 86px" valign="top" >
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Address:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticeAddress1" runat="server" CssClass="TextBoxDataEntry" MaxLength="80" Width="200px" TabIndex="10"/><br />
                <asp:TextBox ID="txtPracticeAddress2" runat="server" CssClass="TextBoxDataEntry" MaxLength="80" Width="200px" TabIndex="11"/>
              </td>
            </tr>
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="City:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticeCity" runat="server" CssClass="TextBoxDataEntry" MaxLength="80" Width="200px" TabIndex="12"/>
              </td>
            </tr>
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="State:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticeState" runat="server" CssClass="TextBoxDataEntry" MaxLength="2" Width="25px" TabIndex="13"/>
              </td>
            </tr>
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Zip:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticeZip" runat="server" CssClass="TextBoxDataEntry" MaxLength="10" Width="65px" TabIndex="14"/>
              </td>
            </tr>
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Phone #1:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticePhone1" runat="server" CssClass="TextBoxDataEntry" MaxLength="12" Width="80px" TabIndex="15" />
              </td>
            </tr>
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Phone #2:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticePhone2" runat="server" CssClass="TextBoxDataEntry" MaxLength="12" Width="80px" TabIndex="16" />
              </td>
            </tr>
            <tr>
              <td style="width: 86px">
                <asp:Label runat="server" CssClass="LabelDataEntry" Text="Fax:" EnableViewState="false"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtPracticeFax" runat="server" CssClass="TextBoxDataEntry" MaxLength="12" Width="80px" TabIndex="17" />
              </td>
            </tr>
          </table>
          </asp:Panel>
        </td>
      </tr>
      <tr>
        <td valign="top" style="width: 51px">&nbsp;
        </td>
        <td valign="top"><asp:CheckBox runat="server" ID="chkIsBSMP" CssClass="CheckBoxDataEntry" Checked="false" Text="Member of BSMP" TabIndex="18" />
        </td>
      </tr>
      <tr>
        <td valign="top" style="width: 51px">&nbsp;
        </td>
        <td valign="top"><asp:CheckBox runat="server" ID="chkIsHRP" CssClass="CheckBoxDataEntry" Checked="false" Text="High Risk Pregnancy doctor" TabIndex="19" />
        </td>
      </tr>
      <tr>
        <td valign="top" style="width: 51px"><asp:Label ID="Label10" runat="server" CssClass="LabelDataEntry" Text="Peer Review:" EnableViewState="false" />
        </td>
        <td valign="top">
        <asp:TextBox ID="txtPeerReview" runat="server" CssClass="TextBoxDataEntry" 
                MaxLength="120" Width="400px" TabIndex="20" TextMode="MultiLine" Rows="2" 
                Columns="20" />
        </td>
      </tr>
    </table>
    <%-- END   Practice dropdown and data entry  --%>
    </td>
    </tr>
    </table>
    <br />
    <table width="100%" cellpadding="1" cellspacing="0">
      <tr>
        <td style="background-color: #404040" colspan="3">
          <asp:Label ID="Label8" runat="server" CssClass="TableHeading" Text="&nbsp;Attitude" />
        </td>
      </tr>
      <tr valign="middle">
        <td valign="top" style="width: 94px">
          <asp:Label ID="Label9" runat="server" CssClass="LabelDataEntry" Text="Overall Attitude:" EnableViewState="false" /></td>
        <td>
          <asp:DropDownList ID="ddlAttitudeCode" runat="server" 
                CssClass="DropDownDataEntry" TabIndex="21">
            <asp:ListItem Value="0" Selected="True" Text="Unknown" />
            <asp:ListItem Value="1" Text="Cooperative" />
            <asp:ListItem Value="2" Text="Favorable" />
            <asp:ListItem Value="3" Text="Favorable with Limitations" />
            <asp:ListItem Value="4" Text="Not Favorable (Do not use)" />
          </asp:DropDownList>&nbsp;
        </td>
        <td valign="middle">
          <b>Choose the most appropriate overall attitude for this Doctor and then check/select the supporting criteria below:</b>
          <br />
        </td>
      </tr>
    </table>
    <table cellpadding="1" cellspacing="0" align="center">
      <tr>
        <td>Is Doctor regularly contacted?</td>
        <td>
          <asp:DropDownList ID="ddlRegContacted" runat="server" CssClass="DropDownDataEntry" TabIndex="22">
            <asp:ListItem Value="0" Selected="True" Text="Unknown" />
            <asp:ListItem Value="1" Text="Yes" />
            <asp:ListItem Value="2" Text="No" />
          </asp:DropDownList>&nbsp;
        </td>
        <td style="width: 30px">&nbsp;</td>
        <td>Does Doctor frequently  treat witnesses?</td>
        <td>
          <asp:DropDownList ID="ddlFrequentlyTreat" runat="server" CssClass="DropDownDataEntry" TabIndex="25">
            <asp:ListItem Value="0" Selected="True" Text="Unknown" />
            <asp:ListItem Value="1" Text="Yes" />
            <asp:ListItem Value="2" Text="No" />
          </asp:DropDownList>&nbsp;
        </td>
      </tr>
      <tr>
        <td>Is Doctor specifically known by you?</td>
        <td>
          <asp:DropDownList ID="ddlSpecificallyKnown" runat="server" CssClass="DropDownDataEntry" TabIndex="23">
            <asp:ListItem Value="0" Selected="True" Text="Unknown" />
            <asp:ListItem Value="1" Text="Yes" />
            <asp:ListItem Value="2" Text="No" />
          </asp:DropDownList>&nbsp;
        </td>
        <td style="width: 30px">&nbsp;</td>
        <td align="right">If so, for how many years? </td>
        <td>
          <asp:TextBox ID="txtTreatYears" runat="server" CssClass="TextBoxDataEntry" MaxLength="2" Width="30px" TabIndex="26" />
        </td>
      </tr>
      <tr>
        <td>Has Doctor been helpful in specific cases?&nbsp;</td>
        <td>
          <asp:DropDownList ID="ddlHelpful" runat="server" CssClass="DropDownDataEntry" TabIndex="24">
            <asp:ListItem Value="0" Selected="True" Text="Unknown" />
            <asp:ListItem Value="1" Text="Yes" />
            <asp:ListItem Value="2" Text="No" />
          </asp:DropDownList>&nbsp;
        </td>
        <td style="width: 30px">&nbsp;</td>
        <td align="right">&nbsp; </td>
        <td>&nbsp;</td>
      </tr>
    </table>
    <br />
    <table width="80%" cellpadding="1" cellspacing="0" align="center">
      <tr>
        <td>
          <asp:CheckBox runat="server" ID="chkFavAdultEmergency" TabIndex="27" Text="Favorable for Adults (Emergency)" />
        </td>
        <td>
          <asp:CheckBox runat="server" ID="chkNotFavChild" TabIndex="32" 
                Text="NOT Favorable for Children" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:CheckBox runat="server" ID="chkFavAdultNonEmergency" TabIndex="28" Text="Favorable for Adults (Non-Emergency)" />
        </td>
        <td>
          <asp:CheckBox runat="server" ID="chkAcceptsMedicaid" TabIndex="33" 
                Text="Willing to Accept Medicaid" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:CheckBox runat="server" ID="chkNotFavAdult" TabIndex="29" 
                Text="NOT Favorable for Adults" />
        </td>
        <td>
          <asp:CheckBox runat="server" ID="chkConsultAdultEmergency" TabIndex="34" 
                Text="Willing to Consult  (Adults)" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:CheckBox runat="server" ID="chkFavChildEmergency" TabIndex="30" 
                Text="Favorable for Children (Emergency)" />
        </td>
        <td>
          <asp:CheckBox runat="server" ID="chkConsultChildEmergency" TabIndex="35" 
                Text="Willing to Consult  (Children)" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:CheckBox runat="server" ID="chkFavChildNonEmergency" TabIndex="31" 
                Text="Favorable for Children (Non-Emergency)" />
        </td>
        <td>
            &nbsp;</td>
      </tr>
    </table>
    <br />
    <table width="100%" cellpadding="1" cellspacing="0">
      <tr>
        <td style="background-color: #404040" colspan="2">
          <asp:Label ID="Label4" runat="server" CssClass="TableHeading" Text="&nbsp;Hospitals" /> &nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Label runat="server" 
                Text="Select a hospital and then enter optional notes to the right (schedule, restrictions, etc.)" 
                Font-Italic="True" ForeColor="White" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlHospital1" runat="server" CssClass="DropDownDataEntry" Width="170px" TabIndex="36">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtHospitalNotes1" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="37" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlHospital2" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="38">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtHospitalNotes2" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="39" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlHospital3" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="40">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtHospitalNotes3" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="41" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlHospital4" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="42">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtHospitalNotes4" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="43" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlHospital5" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="44">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtHospitalNotes5" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="45" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlHospital6" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="46">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtHospitalNotes6" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="47" />
        </td>
      </tr>
    </table>    
    <br />
    <table width="100%" cellpadding="1" cellspacing="0">
      <tr>
        <td style="background-color: #404040" colspan="2">
          <asp:Label ID="Label5" runat="server" CssClass="TableHeading" Text="&nbsp;Specialties" /> &nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Label ID="Label6" runat="server" 
                Text="Select a specialty and then enter optional notes to the right (level of expertise/experience, restrictions, etc.)" 
                Font-Italic="True" ForeColor="White" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlSpecialty1" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="48">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtSpecialtyNotes1" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="49" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlSpecialty2" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="50">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtSpecialtyNotes2" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="51" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlSpecialty3" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="52">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtSpecialtyNotes3" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="53" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlSpecialty4" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="54">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtSpecialtyNotes4" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="55" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlSpecialty5" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="56">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtSpecialtyNotes5" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="57" />
        </td>
      </tr>
      <tr>
        <td style="width: 180px">
          <asp:DropDownList ID="ddlSpecialty6" runat="server" CssClass="DropDownDataEntry" 
                Width="170px" TabIndex="58">
            <asp:ListItem Value="0" Selected="True" Text=" " />
          </asp:DropDownList><br />
        </td>
        <td>
          <asp:TextBox runat="server" ID="txtSpecialtyNotes6" CssClass="TextBoxDataEntry" 
                MaxLength="200" Width="660px" TabIndex="59" />
        </td>
      </tr>
    </table>    
    
    <asp:SqlDataSource ID="sdsPractice" runat="server" ></asp:SqlDataSource>    
    <asp:SqlDataSource ID="sdsStatus" runat="server" ></asp:SqlDataSource>    
    <asp:HiddenField ID="hidMode" runat="server" Value="Edit" /> 
    <asp:HiddenField ID="hidID" runat="server" Value="0" />
</asp:Content>
