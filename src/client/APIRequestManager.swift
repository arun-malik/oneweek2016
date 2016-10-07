//
//  APIRequestManager.swift
//  Realtime Offers
//
//  Created by Dixith Anoop-ANOOP on 7/25/16.
//  Copyright © 2016 microsoft. All rights reserved.
//

import Foundation

class APIRequestManager{

    private func dataTask(request: NSMutableURLRequest, method: String, completion: (success: Bool, object: AnyObject?) -> ()) {
        request.HTTPMethod = method
        
        let session = NSURLSession(configuration: NSURLSessionConfiguration.defaultSessionConfiguration())
        
        session.dataTaskWithRequest(request) { (data, response, error) -> Void in
            if let data = data {
                let json = try? NSJSONSerialization.JSONObjectWithData(data, options: [])
                print("Result json \(json)")
                if let response = response as? NSHTTPURLResponse where 200...299 ~= response.statusCode {
                    completion(success: true, object: json)
                } else {
                    completion(success: false, object: json)
                }
            }
            }.resume()
    }
    
    private func post(request: NSMutableURLRequest, completion: (success: Bool, object: AnyObject?) -> ()) {
        dataTask(request, method: "POST", completion: completion)
    }
    
    private func put(request: NSMutableURLRequest, completion: (success: Bool, object: AnyObject?) -> ()) {
        dataTask(request, method: "PUT", completion: completion)
    }
    
    private func get(request: NSMutableURLRequest, completion: (success: Bool, object: AnyObject?) -> ()) {
        dataTask(request, method: "GET", completion: completion)
    }
    
    private func clientURLRequest(path: String, params: Dictionary<String, AnyObject>? = nil) -> NSMutableURLRequest {
        let request = NSMutableURLRequest(URL: NSURL(string: "http://realtimeoffers.azurewebsites.net/"+path)!)
        do {
            if params != nil {
                request.HTTPBody = try NSJSONSerialization.dataWithJSONObject(params!, options: NSJSONWritingOptions())
                //print("request parameters: \(params)")
            }
        } catch {
            print("bad things happened")
        }
        request.setValue("application/json; charset=utf-8", forHTTPHeaderField: "Content-Type")
  
        return request
    }
    
    func login(phoneNumber: String,  completion: (success: Bool, id: String?) -> ()) {
        let loginObject = ["mobileNumber": phoneNumber]
        
        post(clientURLRequest("api/customers", params: loginObject)) { (success, object) -> () in
            dispatch_async(dispatch_get_main_queue(), { () -> Void in
                if success {
                    print(object!["customerId"])
                    var success: Bool = false
                    var id: String? = nil
                    
                    if let object = object {
                        if let result_number = object.valueForKey("customerId") as? NSNumber
                        {
                            let result_string = "\(result_number)"
                            id = result_string
                            success = true
                        }
                        
                    }
                    completion(success: success, id: id)
                    
                } else {
                    
                    completion(success: false, id: nil)
                }
            })
        }
    }
    
    func getOffers(customerId: String,  completion: (success: Bool, object : AnyObject? ) -> ()) {
        let id = "\(customerId)"
        
        get(clientURLRequest("api/customers/\(id)/offers", params: nil)) { (success, object) -> () in
            dispatch_async(dispatch_get_main_queue(), { () -> Void in
                if success {
                    completion(success: success, object: object)
                    
                } else {
                    
                    completion(success: false, object : nil)
                }
            })
        }
    }
    
    func getUserPreferences(customerId: String, completion: (success: Bool, object : AnyObject? ) -> ()){
        let id = "\(customerId)"
        
        get(clientURLRequest("api/customers/\(id)/preferences", params: nil)) { (success, object) -> () in
            dispatch_async(dispatch_get_main_queue(), { () -> Void in
                if success {
                    completion(success: success, object: object)
                    
                } else {
                    
                    completion(success: false, object : nil)
                }
            })
        }
    }
    
    func getAllPreferences(completion: (success: Bool, object : AnyObject? ) -> ()){
        get(clientURLRequest("api/preferences", params: nil)) { (success, object) -> () in
            dispatch_async(dispatch_get_main_queue(), { () -> Void in
                if success {
                    completion(success: success, object: object)
                    
                } else {
                    
                    completion(success: false, object : nil)
                }
            })
        }
    }
    
    func updateFeedback(customerId: Int, feedbackId: Int, feedback : Int){
        let id = "\(customerId)"
        let feedbackId = "\(feedbackId)"

        
        get(clientURLRequest("api/customers/\(id)/offers/\(feedbackId)/?review=\(feedback)", params: nil)) { (success, object) -> () in
            dispatch_async(dispatch_get_main_queue(), { () -> Void in
                if success {
                    print("update Feedback suceeded")
                } else {
                    
                    print("update Feedback failed \(object)")                }
            })
        }
    }

    
    func updateLocation(customerId: String, latitude: String,  longitude: String) {
        let locationObject = ["customerId": customerId, "latitude" : latitude, "longitude": longitude]
        let id = "\(customerId)"
        
        post(clientURLRequest("api/customers/\(id)/locations", params: locationObject)) { (success, object) -> () in
            dispatch_async(dispatch_get_main_queue(), { () -> Void in
                if success {
                   // print ("location succesful" )
                } else {
                    
                    print ("location unsuccesful \(object)" )                }
            })
        }
    }
}