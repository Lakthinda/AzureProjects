var Crypto = require('crypto');

module.exports = async function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');

    var hmac = Crypto.createHmac("sha1", "aJ8Hqdi9gv/uBDnajepx2PONITOJ49Ni5w99I9cal54FIqm7QSK9bg==");
    var signature = hmac.update(JSON.stringify(req.body)).digest('hex');
    var shaSignature =  `sha1=${signature}`;

    var gitHubSignature = req.headers['x-hub-signature'];

    if (!shaSignature.localeCompare(gitHubSignature)) {
        
        if (req.body.repository.name){
            context.res = {
                body: "Repository is: " + req.body.repository.name + ", Event Type is: " + req.headers['x-github-event']
            };
        }        
        else{
            context.res = {
                status: 400,
                body: "Invalid payload for Wiki event"
            };
        }
    }else{
        context.res = {
            status: 401,
            body: "Signatures don't match"
        };
    }
    

    // const name = (req.query.name || (req.body && req.body.name));
    // const responseMessage = name
    //     ? "Hello, " + name
    //     : "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

    // context.res = {
    //     // status: 200, /* Defaults to 200 */
    //     body: responseMessage
    // };
}