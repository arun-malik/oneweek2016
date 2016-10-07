//
//  SettingsViewController.swift
//  Realtime Offers
//
//  Created by Dixith Anoop-ANOOP on 7/25/16.
//  Copyright © 2016 microsoft. All rights reserved.
//

import UIKit

struct Preference{
    let preferenceId: Int
    let preferenceName : String
}

private var preference: [Preference] = []


class SettingsViewController: UIViewController , UITableViewDataSource, UITableViewDelegate{
    private  let requestManager =  APIRequestManager()
    private var customerId : Int = 0
   
    
    @IBOutlet weak var tableview: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        loadPrefernces();
        tableview.allowsSelection = true;
        // Do any additional setup after loading the view.
    }
   
    
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell{
        
        self.edgesForExtendedLayout = UIRectEdge.None
        
        var cell: UITableViewCell! = tableView.dequeueReusableCellWithIdentifier("Cell") as UITableViewCell!
        
        if (cell == nil){
            cell = UITableViewCell(style: UITableViewCellStyle.Subtitle, reuseIdentifier: "Cell")
        }
        //cell.separatorInset = UIEdgeInsetsZero
        
        //cell.textLabel!.lineBreakMode = NSLineBreakMode.ByWordWrapping
        //cell.textLabel!.numberOfLines = 0;
        //cell.textLabel!.font = UIFont.boldSystemFontOfSize(14.0)
        cell.textLabel!.text = preference[indexPath.row].preferenceName
        
        return cell
    }
    
    func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return preference.count
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func onBackTap(sender: UIButton) {
        if let second = self.presentingViewController?.presentingViewController{
            second.dismissViewControllerAnimated(true, completion: nil)
        }
        else{
            self.dismissViewControllerAnimated(true, completion: nil)
        }
    }

    @IBAction func onSaveTap(sender: UIButton) {
        if let second = self.presentingViewController?.presentingViewController{
            second.dismissViewControllerAnimated(true, completion: nil)
        }
        else{
            self.dismissViewControllerAnimated(true, completion: nil)
        }

    }
    
    override func viewDidAppear(animated: Bool) {
        super.viewDidAppear(true)
        
        let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
        let isLoggedIn:Int = prefs.integerForKey("ISLOGGEDIN") as Int
        if (isLoggedIn != 1) {
            self.performSegueWithIdentifier("goto_login", sender: self)
        } else {
            customerId = prefs.integerForKey("PHONENUMBER") as Int
             
        }
        
    }
    
    func loadPrefernces(){
        requestManager.getAllPreferences { (success, object) in
            if success{
                if let results = object!.valueForKey("results") as? NSArray
                {
                    for result in results{
                        let prefDict = result as? [String:AnyObject]
                        
                        let preferenceId = prefDict!["preferenceId"] as? Int
                        let preferenceName = prefDict!["prefernceName"]! as! String
                        preference.append(Preference(preferenceId: preferenceId!, preferenceName: preferenceName))
                         
                        self.tableview.reloadData()
                    }
                }

        }
    }
        func tableView(tableView: UITableView, willSelectRowAtIndexPath indexPath: NSIndexPath) -> NSIndexPath? {
            let limit = preference.count
            
            if let sr = tableView.indexPathsForSelectedRows {
                if sr.count == limit {
                    let alertController = UIAlertController(title: "Oops", message:
                        "You are limited to \(limit) selections", preferredStyle: .Alert)
                    alertController.addAction(UIAlertAction(title: "OK", style: .Default, handler: {action in
                    }))
                    self.presentViewController(alertController, animated: true, completion: nil)
                    
                    return nil
                }
            }
            
            return indexPath
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
