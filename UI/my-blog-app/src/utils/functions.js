export function uploadImage(loader) {
  return {
    upload: () => {
      return new Promise(async (resolve, reject) => {
        loader.files.then(async (file) => {
          try {
            if (!file) return;

            // Call api upload image to server

            // result {default: urlImage}
            resolve({ default: "" });
          } catch (error) {
            reject(error);
          }
        });
      });
    },
  };
}
