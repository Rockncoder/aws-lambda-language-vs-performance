const mongoClient = require("mongodb").MongoClient;
const uri = process.env.CONNECTION_STRING;
const documentLimit = process.env.DOCUMENT_LIMIT;
const maxDocuments = parseInt(documentLimit);
let cachedDb = null;

async function connectToDatabase() {
  if (cachedDb) {
    console.info('used cached db connection');
    return cachedDb;
  }

  const client = await mongoClient.connect(uri);
  const db = client.db(process.env.DB_NAME);
  cachedDb = db;
  return db;
}

module.exports.vehicles = async (event, context) => {
  context.callbackWaitsForEmptyEventLoop = false;
  const db = await connectToDatabase();
  const vehicles = await db.collection(process.env.COLLECTION_NAME).find({}, { projection: { year: 1, make: 1, model: 1, _id: 0 } }).limit(maxDocuments).toArray();

  const response = {
    statusCode: 200,
    body: JSON.stringify(vehicles),
  };

  return response;
};
