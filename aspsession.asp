<%
function jsonReturn(status,message,data)
	dim json
	json = "{" &_
		        """status"":" & status & ","&_ 
		        """message"": """ & message & ""","&_ 
		        """data"": " & data & ""&_ 
		    "}"
	response.Write(json)
	response.End()
end function

function deleteLastComma(valStr)
	on error resume next
	lastCommaPos = instrrev(valStr, ",")
	valStr = mid(valStr, 1, lastCommaPos - 1)
	deleteLastComma = valStr
end function

Response.ContentType = "application/json"
Dim strJSON, sessionLenght
sessionLenght = 0

if (request.servervariables("LOCAL_ADDR") = request.servervariables("REMOTE_ADDR")) then
    strJSON = "{" &_
                    """SessionId"":""" & Session.SessionID & """," &_
                    """SessionVariables"":{" 
                    For Each SessionVar In Session.Contents
                        strJSON = strJSON & """" & SessionVar & """: """ & session(SessionVar) & ""","
                        sessionLenght = sessionLenght + 1
                    Next
                    If sessionLenght > 0 Then
                        strJSON = deleteLastComma(strJSON)
                    End If
                    strJSON = strJSON & "}"&_
                "}"
    jsonReturn "200","Session Info","[" & strJSON & "]"
else
    jsonReturn "401", "Unauthorized","[]"
end if
%>