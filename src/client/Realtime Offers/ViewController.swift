//
//  ViewController.swift
//  Realtime Offers
//
//  Created by Dixith Anoop-ANOOP on 7/25/16.
//  Copyright © 2016 microsoft. All rights reserved.
//

import UIKit
import CoreLocation

struct Offer{
    let id: Int
    let offerTag : String
    let description : String
    let offerFeedbackId : Int
}

class ViewController: UIViewController , UITableViewDataSource,UITableViewDelegate, CLLocationManagerDelegate{
    
    private var offers: [Offer] = []
    private  let requestManager =  APIRequestManager()
    private var customerId : Int = 0
    let locationManager = CLLocationManager()
    var timer : NSTimer?
    var refreshTimer : NSTimer?
    private var selectedOffer:Offer = Offer(id: 0, offerTag: "", description: "" , offerFeedbackId: 0)
    private var selectedOfferRow: Int = 0
    var refreshControl: UIRefreshControl!
    
    @IBOutlet weak var tableView: UITableView!
    
    private var images: [String] = ["img1-100.jpg",
                                    "img2-100.jpg",
                                    "img3-100.jpg",
                                    "img4-100.jpg",
                                    "img5-100.jpg",
                                    "img6-100.jpg",
                                    "img7-100.jpg",
                                    "img8-100.jpg",
                                    "img9-100.jpg",
                                    "img10-100.jpg",
                                    ]

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        startTimer()
        tableView.allowsSelection = true;
        refreshControl = UIRefreshControl()
        refreshControl.attributedTitle = NSAttributedString(string: "Pull to refresh")
        refreshControl.addTarget(self, action: #selector(ViewController.refreshTable), forControlEvents: UIControlEvents.ValueChanged)
        tableView.addSubview(refreshControl)    }
    
    func didEnterBackground() {
        self.timer!.invalidate()
        self.refreshTimer!.invalidate()
    }
    
    func didBecomeActive() {
        startTimer()
        refreshTable()
    }
    
    func startTimer(){
        if (timer == nil){
            timer = NSTimer()
            timer = NSTimer.scheduledTimerWithTimeInterval(10.0, target: self, selector: #selector(ViewController.updateLocation), userInfo: nil, repeats: true)
        }
        if (refreshTimer == nil){
            refreshTimer = NSTimer()
            refreshTimer = NSTimer.scheduledTimerWithTimeInterval(10.0, target: self, selector: #selector(ViewController.refreshTable), userInfo: nil, repeats: true)
        }
    }
    
    func updateLocation(){
        // Ask for Authorisation from the User.
        locationManager.requestAlwaysAuthorization()
        
        // For use in foreground
        locationManager.requestWhenInUseAuthorization()
        
        if CLLocationManager.locationServicesEnabled() {
            locationManager.delegate = self
            locationManager.desiredAccuracy = kCLLocationAccuracyNearestTenMeters
            locationManager.startUpdatingLocation()
        }

    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    
    @IBAction func onSettingsTap(sender: UIButton) {
        self.performSegueWithIdentifier("goto_settings_home", sender: self)
    }
    
    @IBAction func onLogoutTap(sender: UIButton) {
        
        let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
        prefs.setInteger(0, forKey: "ISLOGGEDIN")
        prefs.synchronize()
        self.performSegueWithIdentifier("goto_login", sender: self)
    }
    
    override func viewDidAppear(animated: Bool) {
        super.viewDidAppear(true)
        
        let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
        let isLoggedIn:Int = prefs.integerForKey("ISLOGGEDIN") as Int
        if (isLoggedIn != 1) {
            self.performSegueWithIdentifier("goto_login", sender: self)
        } else {
            customerId = prefs.integerForKey("PHONENUMBER") as Int
            
            print(customerId)
            refreshTable()
        }
        
    }
    
    func tableView(tableView: UITableView, cellForRowAtIndexPath indexPath: NSIndexPath) -> UITableViewCell{
        
        self.edgesForExtendedLayout = UIRectEdge.None
        
        var cell: UITableViewCell! = tableView.dequeueReusableCellWithIdentifier("Cell") as UITableViewCell!
        
        if (cell == nil){
            cell = UITableViewCell(style: UITableViewCellStyle.Subtitle, reuseIdentifier: "Cell")
        }
        cell.separatorInset = UIEdgeInsetsZero
        
        cell.textLabel!.lineBreakMode = NSLineBreakMode.ByWordWrapping
        cell.textLabel!.numberOfLines = 0;
        cell.textLabel!.font = UIFont.boldSystemFontOfSize(14.0)
    
        cell.textLabel!.text = offers[indexPath.row].offerTag
        cell.detailTextLabel!.text = offers[indexPath.row].description
        cell.detailTextLabel!.numberOfLines = 0;
        cell.detailTextLabel!.font = UIFont.systemFontOfSize(12.0)
        
        // Do not show image if there is no result
        if (offers[indexPath.row].id != -1){
            let imageName = images[indexPath.row % 10]
            let image = UIImage(named: imageName)
            cell.imageView!.image = image
            let itemSize = CGSizeMake(80, 80);
            UIGraphicsBeginImageContextWithOptions(itemSize, false, UIScreen.mainScreen().scale);
            let imageRect = CGRectMake(0.0, 0.0, itemSize.width, itemSize.height);
            cell.imageView?.image!.drawInRect(imageRect)
            cell.imageView?.image! = UIGraphicsGetImageFromCurrentImageContext();
            UIGraphicsEndImageContext();
        }
        
        return cell
    }
    
    func tableView(tableView: UITableView, didSelectRowAtIndexPath indexPath: NSIndexPath){
        let row = indexPath.row
        selectedOffer = Offer(id: offers[row].id, offerTag: offers[row].offerTag, description: offers[row].description, offerFeedbackId: offers[row].offerFeedbackId)
        selectedOfferRow = row
        self.performSegueWithIdentifier("goto_offer", sender: self)
    }
    
    func tableView(tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return offers.count
    }
    
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        
        if(segue.identifier == "goto_offer") {
            
            let offerVC = (segue.destinationViewController as! OfferViewController)
            offerVC.offerData = selectedOffer
            offerVC.imageIndex = selectedOfferRow
        }
    }

    func locationManager(manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
        let locValue:CLLocationCoordinate2D = manager.location!.coordinate
        //print("locations = \(locValue.latitude) \(locValue.longitude)")
        manager.stopUpdatingLocation()
        self.requestManager.updateLocation(String(self.customerId), latitude: "\(locValue.latitude)", longitude: "\(locValue.longitude)")
        
    }
    
    func refreshTable(){
        
        requestManager.getOffers(String(customerId)) { (success, object) -> () in
            if success{
                print("refresh \(object)")
                if let results = object!.valueForKey("results") as? NSArray
                {
                    self.offers.removeAll()
                    for result in results{
                        let offerdict = result as? [String:AnyObject]
                        let offerId = offerdict!["offerId"] as? Int
                        let offerFeedbackId = offerdict!["offerFeedbackId"] as? Int
                        let offer = offerdict!["offer"]! as! [String:String]
                        let offerTag = offer["offerTag"]! as String
                        let offerDescription = offer["offerDescription"]! as String
                        self.offers.append(Offer(id: offerId!, offerTag : offerTag,  description: offerDescription, offerFeedbackId: offerFeedbackId!))
                    }
                }
                if (self.offers.count == 0){
                    self.offers.append(Offer(id: -1, offerTag : "No new Offers",  description: "Please come back later", offerFeedbackId: -1))
                }
                self.tableView.reloadData()
                
            }else{
                
            }
        }
        refreshControl.endRefreshing()
    }
    
}

