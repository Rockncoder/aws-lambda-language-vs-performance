import json
import os
from base64 import b64decode
from pymongo import MongoClient

uri = os.environ["CONNECTION_STRING"]
DB_NAME = os.environ["DB_NAME"]
COLLECTION_NAME = os.environ["COLLECTION_NAME"]
DOCUMENT_LIMIT = os.environ["DOCUMENT_LIMIT"]
maxDocuments = int(DOCUMENT_LIMIT)


def main(event, context):
    client = MongoClient(uri)
    db = client[DB_NAME]
    collection = db[COLLECTION_NAME].find({},{"year":1,"make":1,"model":1, "_id":0}).limit(maxDocuments)
    results = list(collection)
    print(results)

    response = {
        "statusCode": 200,
        "body": json.dumps(results)
    }

    return response

if __name__ == "__main__":
    main('', '')
