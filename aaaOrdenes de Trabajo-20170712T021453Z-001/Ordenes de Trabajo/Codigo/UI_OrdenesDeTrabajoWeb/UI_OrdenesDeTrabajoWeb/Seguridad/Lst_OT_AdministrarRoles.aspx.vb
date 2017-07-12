Imports Utilerias.OrdenesDeTrabajo

Partial Class Seguridad_Lst_OT_AdministrarRoles
    Inherits System.Web.UI.Page

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro Propio del evento</param>
    ''' <param name="e">Parámetro Propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Buscar()
            End If
            Me.pnRpRoles.Dibujar()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar rol
    ''' </summary>
    ''' <param name="sender">Parámetro Propio del evento</param>
    ''' <param name="e">Parámetro Propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If Borrar(CType(sender, ImageButton).CommandArgument) Then
                MostrarAlertaRegistroBorrado()
                Buscar()
            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el repeter 'rpRoles', por cada dato del repeater
    ''' se asigna un atributo, en este caso para la confirmación de borrado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpRoles_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpRoles.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Obtiene los diferentes roles del sistema y los carga el repeater de roles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar()
        Dim vlo_WebUtil As New WebUtils
        Dim vlb_HayDatos As Boolean
        Dim vlo_Datos As String()
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_Role As String

        Try
            vlb_HayDatos = False

            vlo_Datos = Roles.GetAllRoles()

            Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de registros: {0}", vlo_Datos.Count)
            vlo_DsDatos = New System.Data.DataSet
            vlo_DsDatos.Tables.Add(New System.Data.DataTable())
            vlo_DsDatos.Tables(0).Columns.Add(New System.Data.DataColumn("RoleName", GetType(String)))
            For Each vlo_Role In vlo_Datos
                vlo_DsDatos.Tables(0).Rows.Add(New Object() {vlo_Role.Trim})
            Next

            If vlo_Datos.Count > 0 Then

                With Me.rpRoles
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With

                Me.lblCantidadDeRegistros.Visible = True
            Else

                With Me.rpRoles
                    .DataSource = vlo_DsDatos
                    .DataBind()
                End With
                MostrarAlertaNoHayDatos()

            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de borrar un rol, segun el rolname que obtenga por parámetro
    ''' </summary>
    ''' <param name="pvc_Rolename"></param>
    ''' <returns>true: En caso de acción exitosa, false: En caso de no realizar la acción</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(ByVal pvc_Rolename As String) As Boolean
        Try
            Return Roles.DeleteRole(pvc_Rolename)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

#End Region

End Class
