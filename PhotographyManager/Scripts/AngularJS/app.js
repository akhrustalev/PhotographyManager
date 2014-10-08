var app = angular.module('app', []); app.value('$', $);


function SignalRAngularCtrl($scope, signalRSvc) {
    $scope.Comments = []
    $scope.PushComment = function (photoId) {
        var commentText = $scope.commentText;
        alert('pushed');
        $scope.Comments.push({ text: commentText, photoId: photoId });
        //signalRSvc.sendRequest(commentText, photoId);
    }
    updateComment = function (text,photoId) {     
        $scope.Comments.push({text:text,photoId:photoId});    
    }
    signalRSvc.initialize(updateComment);
}
app.factory('signalRSvc', function ($, $rootScope) {    
    return {       
        proxy: null,       
        initialize: function (acceptCommentCallback) {             //Getting the connection object     
            connection = $.hubConnection();                //Creating proxy            
            this.proxy = connection.createHubProxy('CommentsHub');                //Starting connection             
            connection.start();                    
            this.proxy.on('acceptComment', function (message,photoId) {           
                $rootScope.$apply(function () {                  
                    acceptCommentCallback(message,photoId);              
                });          
            });       
        },        
        sendRequest: function (commentText,photoId) {      
            this.proxy.invoke("PushComment",commentText,photoId);
        }
    }
});

//function SignalRAngularCtrl($scope, signalRSvc, $rootScope) {    
//    $scope.Comments = [];      
//    $scope.PushComment = function (photoId) {        
//        alert('pushed');
//        //signalRSvc.sendRequest();   
//    }      
//    updateComment = function (text,photoId) {     
//                $scope.Comments.push({text:text,photoId:photoId});    
//           }    
//    signalRSvc.initialize();        //Updating greeting message after receiving a message through the event    
//    $scope.$parent.$on("acceptComment", function (e, message,photoId) {
//        $scope.$apply(function () {
//            updateComment(message,photoId)
//        });
//    });
//}

//app.service('signalRSvc', function ($, $rootScope) {  
//    var proxy = null;  
//    var initialize = function () {         //Getting the connection object       
//        connection = $.hubConnection();            //Creating proxy    
//        this.proxy = connection.createHubProxy('helloWorldHub');            //Starting connection     
//        connection.start();            //Publishing an event when server pushes a greeting message        
//        this.proxy.on('acceptComment', function (message,photoId) {          
//            $rootScope.$emit("acceptComment",message,photoId);       
//        });  
//    };     
//    var sendRequest = function () {         //Invoking greetAll method defined in hub       
//        this.proxy.invoke('PushComment', commentText, photoId);
//    }; return { initialize: initialize, sendRequest: sendRequest };
//});