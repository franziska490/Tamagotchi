from flask import Flask, jsonify, request
app = Flask(__name__)



@app.route("/pets",methods=["GET"])
def get_pets():
    pets = [
        {"petid": 1, "name": "Chubby", "hunger": 85, "energy": 70, "mood": 90, "ownerid": 1},
        {"petid": 2, "name": "Sid", "hunger": 45, "energy": 30, "mood": 60, "ownerid": 1},
        {"petid": 3, "name": "Fluffy", "hunger": 20, "energy": 15, "mood": 40, "ownerid": 2}
    ]

    return jsonify(pets),200


if __name__=='__main__':
    app.run()
