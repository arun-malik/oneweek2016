//
//  OfferViewController.swift
//  Realtime Offers
//
//  Created by Dixith Anoop-ANOOP on 7/26/16.
//  Copyright © 2016 microsoft. All rights reserved.
//

import UIKit

class OfferViewController: UIViewController {

    var offerData : Offer = Offer(id: 0,  offerTag: "", description: "", offerFeedbackId: 0)
    var imageIndex : Int = 0
    
    private var images: [String] = ["Image-1-300.jpg",
                                    "Image-2-300.jpg",
                                    "Image-3-300.jpg",
                                    "Image-4-300.jpg",
                                    "Image-5-300.jpg",
                                    "Image-6-300.jpg",
                                    "Image-7-300.jpg",
                                    "Image-8-300.jpg",
                                    "Image-9-300.jpg",
                                    "Image10-300.jpg",
                                    ]
    private  let requestManager =  APIRequestManager()

  
    @IBOutlet weak var offerDescription: UILabel!
    
    @IBOutlet weak var offerImage: UIImageView!
    
    @IBOutlet weak var offerTag: UILabel!
    
    
    @IBOutlet weak var likeButton: UIButton!
    
    @IBOutlet weak var dislikeButton: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
        
        offerDescription.text = offerData.description
        offerTag.text = offerData.offerTag
        
        let imageName = images[imageIndex]
        let image = UIImage(named: imageName)
        offerImage!.image = image
      

    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    @IBAction func onBackButtonTap(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)

    }
      
    @IBAction func onLikeTap(sender: UIButton) {
        likeButton.setTitle("Liked", forState: UIControlState.Normal)
        dislikeButton.setTitle("Dislike", forState: UIControlState.Normal)
    
        let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
        let customerId = prefs.integerForKey("PHONENUMBER") as Int
        requestManager.updateFeedback(customerId , feedbackId: offerData.offerFeedbackId, feedback : 1)
    }
    
    @IBAction func onDislikeTap(sender: UIButton) {
        dislikeButton.setTitle("Disliked", forState: UIControlState.Normal)
        likeButton.setTitle("Like", forState: UIControlState.Normal)
        
        let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
        let customerId = prefs.integerForKey("PHONENUMBER") as Int
        requestManager.updateFeedback(customerId , feedbackId: offerData.offerFeedbackId, feedback : 2)
    }
    
    
    @IBAction func onNeverShowTap(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)
        
        let prefs:NSUserDefaults = NSUserDefaults.standardUserDefaults()
        let customerId = prefs.integerForKey("PHONENUMBER") as Int
        requestManager.updateFeedback(customerId , feedbackId: offerData.offerFeedbackId, feedback : 3)


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
