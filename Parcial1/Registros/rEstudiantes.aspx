<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="rEstudiantes.aspx.cs" Inherits="Parcial1.Registros.rEstudiantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
      <div class="card">
        <div class="panel" style="background-color: #0094ff">
            <div class="panel-heading" style="font-family: Arial Black; font-size: 20px; text-align: center; color: Black">Registro de estudiantes</div>
        </div>

        <div class="panel-body">
            <div class="card-body">
                <div class="container">
                    <div class="form-row">

                    <%--EstudianteID--%>
                          <div class="col-md-2 col-md-offset-3">
                            <asp:Label ID="EstudianteIdLabel" Text="EstudianteId" runat="server" />
                            <asp:TextBox ID="EstudianteIdTextBox" class="form-control input-sm" TextMode="Number" runat="server" Text="0" placeholder="0"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVId" ValidationGroup="Buscar" ControlToValidate="EstudianteIdTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        
                              <%--BuscarButton--%>
                        <div class="col-md-2 col-sm-2 col-xs-2">
                            <div class="input-group-append">
                                <br />
                                <asp:Button Text="Buscar" class="btn btn-primary" runat="server" ID="BuscarButton" OnClick="BuscarButton_Click" />
                            </div>
                            </div>
                           
                        <%--FechaRegistro--%>
                        <div class="col-md-2  col-xs-3 col-md-offset-1">
                           <label for="fechaTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Fecha</label>
                            <asp:TextBox ID="fechaTextBox" class="form-control input-sm" TextMode="Date" runat="server"></asp:TextBox>
                        </div>

               

                    <%--Nombres--%>
                        <br />
                     <div class="col-md-3 col-md-offset-3">
                        <label for="NombresTextBox" class="col-md-3 control-label input-sm" style="font-size: small">Nombres</label>
                            <asp:TextBox ID="NombresTextBox" runat="server" class="form-control input-sm" Style="font-size: small"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="Valida" runat="server" ErrorMessage="El campo &quot;Nombres&quot; esta vacio" ControlToValidate="NombresTextBox" ForeColor="Red" Display="Dynamic" ToolTip="Campo Nombres es obligatorio" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    </div>

                    </div>

                </div>
            </div>
        </div>

         <%--Botones--%>
         <div class="panel">
             <div class="text-center">
                 <div class="form-group">
                     <asp:Button ID="NuevoButton" runat="server" Text="Nuevo" class="btn btn-primary" OnClick="NuevoButton_Click" />
                     <asp:Button ID="GuardarButton" runat="server" Text="Guardar" class="btn btn-success" ValidationGroup="Guardar" OnClick="GuardarButton_Click1"  />
                     <asp:Button ID="EliminarButton" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="EliminarButton_Click1"  />
                 </div>
             </div>
         </div>
    </div>

          </div>
    

</asp:Content>