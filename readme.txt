***************************************
 __                   __        __      
|_  _ . _  _    _    |_  _  _  /  \ /\  
|__|_)|| )(_)\/(_|.  |__(_|_)\/\_\//--\ 
   |                         /         

*************************************** 

This originated as a tool for our inhouse QA-process at Epinova. 

It's basically an advanced checklist. You create your checklist criterias, and create a new "QA" (Quality Assurance) as we call it. For every criteria (your project) has fulfilled, mark it with a green check or red error-sign. Add comments etc. to every point.

Whoever (users come from your membership provider) is set as project member can later mark a failed criteria as fixed/won't fix.

It's all based around snappy AJAX-interaction. There are no explicit save-buttons anywhere. Everything is supposed to be click-BAM-SAVED RIGHT AWAY!

It might not make sense for you, but it does (hopefully) work for us.


1. Uses RavenDB - http://ravendb.net/

2. Connection strings must be set in web.config

3. Use authentication if you want to, and provide some sort of membershipprovider. This app comes with nothing by default. To do simple testing, use Windows authentication.