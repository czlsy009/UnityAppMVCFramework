using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using BlankFramework;
using FairyGUI;
using UnityEngine;
using StartPage;
public class StartPageUIViewController : UIViewController
{
    private UIStartPage startPage;
    protected override void ViewDidAppear(EventContext eventContext)
    {
        base.ViewDidAppear(eventContext);
        InitView();
        BindEvents();
    }

    private void BindEvents()
    {
        startPage.mLoginButton.onClick.Add((() =>
        {
            Dictionary<string, string> pas = new Dictionary<string, string>();
            pas.Add("templateId", "1038343388014780418");
            NetWorkManager.Instance.SendPostType("http://java.3plus.ltd:8087/" + "api/template/get",
                (successMessage =>
                {
                    HTTPResponse res = (HTTPResponse)successMessage.Body;
                    Debug.Log(res.DataAsText);
                }), (errorMessage =>
                 {

                 }), pas);
        }));
    }

    private void InitView()
    {
        startPage = UIStartPage.CreateInstance();
        View.AddChild(startPage);
    }
}
