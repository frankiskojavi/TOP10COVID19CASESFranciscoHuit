<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WpgReport.aspx.cs" Inherits="TOP10COVID19CASESFranciscoHuit.Views.WpgReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<!-- Bootstrap -->
 <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>    
<body>
    <form id="form1" runat="server">      
        <center>
          <h2>TOP 10 COVID CASES</h2>                 
        </center>

        <div class="card">
          <div class="card-body">
            <div class="form-row">
                <div class="form-group col-md-2">
                     <asp:DropDownList ID="dpdRegions" runat="server" AppendDataBoundItems="true" CssClass="form-control" Style="width: 200px">                         
                         <asp:ListItem Text="Regions" Value="Regions" />
                     </asp:DropDownList>
                </div>
                <div class="form-group col-md-2">
                    <asp:Button ID="Button5" runat="server" Text="Report" CssClass="btn btn-secondary btn-block" onclick="Button5_Click"/>
                </div>
                <div class="form-group col-md-6">
                </div>
                <div class="form-group col-md-2">
                    <asp:Button ID="btnXML" runat="server" Text="XML" CssClass="btn btn-secondary" onclick="btnXML_Click"/>
                    <asp:Button ID="btnJSON" runat="server" Text="JSON" CssClass="btn btn-secondary" OnClick="btnJSON_Click" />
                    <asp:Button ID="btnCSV" runat="server" Text="CSV" CssClass="btn btn-secondary" OnClick="btnCSV_Click"/>                    
                </div>
            </div>
          </div>
           <% if (grdView2.Rows.Count == 0 || dpdRegions.Items.Count == 0) {  %>
                <div class="alert alert-danger" role="alert">
                  <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>          
                </div>
           <% }%>  
        </div>
        <asp:GridView ID="grdView2" runat="server" AutoGenerateColumns="true"  CssClass="table table-bordered text-center">                
        </asp:GridView> 

    </form>
</body>
</html>
