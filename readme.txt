Development on this has kind of halted.
A new version might appear in the future, somewhere.

***************************************
 __                   __        __      
|_  _ . _  _    _    |_  _  _  /  \ /\  
|__|_)|| )(_)\/(_|.  |__(_|_)\/\_\//--\ 
   |                         /         

*************************************** 

This originated as a tool for our inhouse QA-process at Epinova. 

It's basically an advanced checklist. You create your checklist criterias, and create a new "QA" (Quality Assurance), as we call it. For every criteria (your project has) fulfilled or failed at, mark it with a green check or red error-sign. Or a blue question mark if you're just not sure. You can also add comments etc. to every point.

Whoever (users come from a membership provider of your choice) is set as project member can later mark a failed criteria as fixed/won't fix.


1. Uses RavenDB - http://ravendb.net/
  -- Seems like licensing rules have changed quite drastically since this project spawned. Should be looked into.
  Ayende has, however, said that projects deployed before this change is not affected (https://groups.google.com/forum/#!msg/ravendb/bDyQeteiciU/0AiHEbeLJ4AJ)

2. Connection strings must be set in web.config

3. Use authentication if you want to, and provide some sort of membershipprovider. This app comes with nothing by default. To do simple testing, use Windows authentication.
