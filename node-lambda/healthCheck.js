module.exports.healthCheck = async (event, context) => {
  return {
    statusCode: 200,
    body: JSON.stringify(
      {
        message: 'Node is OK.',
        input: event,
      },
      null,
      2
    ),
  };
};
