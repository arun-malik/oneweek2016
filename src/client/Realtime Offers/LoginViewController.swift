//
//  LoginViewController.swift
//  Realtime Offers
//
//  Created by Dixith Anoop-ANOOP on 7/25/16.
//  Copyright © 2016 microsoft. All rights reserved.
//

import UIKit

class LoginViewController: UIViewController {

 
    
    @IBOutlet weak var userPhoneNumber: UITextField!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    private func showAlert(title: String, msg: String){
        let alert=UIAlertController(title: title, message: msg, preferredStyle: UIAlertControllerStyle.Alert);
        alert.addAction(UIAlertAction(title: "OK", style: UIAlertActionStyle.Cancel, handler: nil));
        //show it
        showViewController(alert, sender: self);

        
    }
    
    @IBAction func OnLoginTap(sender: UIButton) {
        
        let phoneNumber: NSString = userPhoneNumber.text!
        
        if (phoneNumber.isEqualToString("") ){
            showAlert("Error", msg: "Enter phone number")
            
       // }//else if (phoneNumber.length < 10 || phoneNumber.length > 11){
           // showAlert("Error", msg: "Invalid phone number")
        }else{
            let requestManager =  APIRequestManager()
            
            requestManager.login(phoneNumber as String) { (success, customerId) -> () in
                if success {
                    let userId: String = customerId!
                    print(userId)
                    
                    let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
                    prefs.setObject(userId, forKey: "PHONENUMBER")
                    prefs.setInteger(1, forKey: "ISLOGGEDIN")
                    prefs.synchronize()
                    
                    self.performSegueWithIdentifier("goto_settings_login", sender: self)
                    
                } else {
                    self.showAlert("Error", msg: "Something went wrong. Please try again")
                }
            }
        }
    }
    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */

}
