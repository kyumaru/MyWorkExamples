///-----------------------------------------------------------------
///
/// @file      Project1Frm.h
/// @author    kyumaru
/// Created:   11/21/2012 5:53:19 AM
/// @section   DESCRIPTION
///            Project1Frm class declaration
///
///------------------------------------------------------------------

#ifndef __PROJECT1FRM_H__
#define __PROJECT1FRM_H__

#ifdef __BORLANDC__
	#pragma hdrstop
#endif

#ifndef WX_PRECOMP
	#include <wx/wx.h>
	#include <wx/frame.h>
#else
	#include <wx/wxprec.h>
#endif

//Do not add custom headers between 
//Header Include Start and Header Include End.
//wxDev-C++ designer will remove them. Add custom headers after the block.
////Header Include Start
#include <wx/button.h>
#include <wx/textctrl.h>
#include <wx/panel.h>
#include <wx/notebook.h>
////Header Include End

////Dialog Style Start
#undef Project1Frm_STYLE
#define Project1Frm_STYLE wxCAPTION | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxCLOSE_BOX
////Dialog Style End

class Project1Frm : public wxFrame
{
	private:
		DECLARE_EVENT_TABLE();
		
	public:
        //wxPanel *WxPanel1;
		Project1Frm(wxWindow *parent, wxWindowID id = 1, const wxString &title = wxT("Project1"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxDefaultSize, long style = Project1Frm_STYLE);
		virtual ~Project1Frm();
		void WxNotebook1PageChanged(wxNotebookEvent& event);
		void WxNoteBookPage1UpdateUI(wxUpdateUIEvent& event);
		void WxNoteBookPage2UpdateUI(wxUpdateUIEvent& event);
		void WxNoteBookPage3UpdateUI(wxUpdateUIEvent& event);
		void WxButton2Click(wxCommandEvent& event);
		void WxButton1Click(wxCommandEvent& event);
		void WxEdit2Updated(wxCommandEvent& event);
		void WxButton4Click(wxCommandEvent& event);
		void WxButton3Click(wxCommandEvent& event);
		void WxButton6Click(wxCommandEvent& event);
		void WxButton5Click(wxCommandEvent& event);
		
	public:
		//Do not add custom control declarations between
		//GUI Control Declaration Start and GUI Control Declaration End.
		//wxDev-C++ will remove them. Add custom code after the block.
		////GUI Control Declaration Start
		wxTextCtrl *WxEdit19;
		wxButton *WxButton6;
		wxTextCtrl *WxEdit18;
		wxTextCtrl *WxEdit17;
		wxTextCtrl *WxEdit16;
		wxTextCtrl *WxEdit15;
		wxPanel *WxPanel3;
		wxButton *WxButton5;
		wxTextCtrl *WxEdit14;
		wxTextCtrl *WxEdit13;
		wxTextCtrl *WxEdit12;
		wxTextCtrl *WxEdit11;
		wxTextCtrl *WxEdit10;
		wxTextCtrl *WxEdit9;
		wxPanel *WxPanel2;
		wxButton *WxButton4;
		wxButton *WxButton3;
		wxTextCtrl *WxEdit8;
		wxTextCtrl *WxEdit7;
		wxTextCtrl *WxEdit6;
		wxTextCtrl *WxEdit5;
		wxButton *WxButton2;
		wxTextCtrl *WxEdit4;
		wxPanel *WxPanel1;
		wxButton *WxButton1;
		wxTextCtrl *WxEdit3;
		wxPanel *WxNoteBookPage3;
		wxTextCtrl *WxEdit2;
		wxPanel *WxNoteBookPage2;
		wxTextCtrl *WxEdit1;
		wxPanel *WxNoteBookPage1;
		wxNotebook *WxNotebook1;
		////GUI Control Declaration End
		
	private:
		//Note: if you receive any error with these enum IDs, then you need to
		//change your old form code that are based on the #define control IDs.
		//#defines may replace a numeric value for the enum names.
		//Try copy and pasting the below block in your old form header files.
		enum
		{
			////GUI Enum Control ID Start
			ID_WXEDIT19 = 1033,
			ID_WXBUTTON6 = 1032,
			ID_WXEDIT18 = 1031,
			ID_WXEDIT17 = 1030,
			ID_WXEDIT16 = 1029,
			ID_WXEDIT15 = 1028,
			ID_WXPANEL3 = 1027,
			ID_WXBUTTON5 = 1026,
			ID_WXEDIT14 = 1025,
			ID_WXEDIT13 = 1024,
			ID_WXEDIT12 = 1023,
			ID_WXEDIT11 = 1022,
			ID_WXEDIT10 = 1021,
			ID_WXEDIT9 = 1020,
			ID_WXPANEL2 = 1019,
			ID_WXBUTTON4 = 1018,
			ID_WXBUTTON3 = 1017,
			ID_WXEDIT8 = 1016,
			ID_WXEDIT7 = 1015,
			ID_WXEDIT6 = 1014,
			ID_WXEDIT5 = 1013,
			ID_WXBUTTON2 = 1012,
			ID_WXEDIT4 = 1011,
			ID_WXPANEL1 = 1010,
			ID_WXBUTTON1 = 1005,
			ID_WXEDIT3 = 1036,
			ID_WXNOTEBOOKPAGE3 = 1004,
			ID_WXEDIT2 = 1035,
			ID_WXNOTEBOOKPAGE2 = 1003,
			ID_WXEDIT1 = 1034,
			ID_WXNOTEBOOKPAGE1 = 1002,
			ID_WXNOTEBOOK1 = 1001,
			////GUI Enum Control ID End
			ID_DUMMY_VALUE_ //don't remove this value unless you have other enum values
		};
		
	private:
		void OnClose(wxCloseEvent& event);
		void CreateGUIControls();
};

#endif
