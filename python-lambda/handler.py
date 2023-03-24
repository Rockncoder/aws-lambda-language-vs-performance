import json


def healthcheck(event, context):
    body = {
        "message": "Python is OK.",
        "input": event
    }

    response = {
        "statusCode": 200,
        "body": json.dumps(body)
    }

    return response
