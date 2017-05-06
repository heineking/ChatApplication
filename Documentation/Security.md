## Security ##

The following security features were implemented in this project:

* Password Hashing
* Brute Force Protection
* JSON Web Token
* SSL (not yet implemented)

***

### Password Hashing ###

**see:** `ChatApplication.Password` for the implementation.

The Chat Application uses **salted password hashing** along with a keyed-hash message authentication message (**HMAC**) to securely store the password in the login database.

A unique 32 character length salt is generated for each password and prepended to the user's password. The random salt makes it impossible to use look up tables or rainbow tables for password cracking. The salted password is also hashed and stretched using a **PBKDF2** implementation.

The **PBKDF2** is a hashing algorithm that makes it harder to brute force attack a password by adding linear time to how long it takes to compute a key. The ChatApplication targets 50,000 iterations for the algorithm which on my machine equates to ~500ms to compute the key. This would make any brute-force attack extremely expensive.

A **HMACSHA256** further secures the salted-password hash by hashing the password again but against a private 32 character length key. This approach makes it impossible for the passwords to be cracked without the private key. A positive to this approach is that the key does not need to be stored in the database. An attack would have to compromise both the user account database and whatever system is being used to store the key.

It should be noted that this does not protect a user against using a weak password. A password of **password1** is still poor for security. A hacker will be able to compromise this account with relative ease. It's also important to avoid storing the passwords in plaintext because many users share passwords across systems, and any breach could compromise those other systems.

#### Sources ####

https://crackstation.net/hashing-security.htm

***

### Brute Force Protection ###

**see:** `BruteForceDecorator` in `ChatApplication.Security.Security` for the implementation

A brute force attack is where a user's account is compromised by an attacker trying many passwords with the hopes of guessing right.

The `LoginRecord` in the database keeps track of how many failed login attempts are made for a specific username. When a login fails the `LoginRecord.AttemptedLogins` is incremented by one. All logins, including with a valid password, are disabled after login fails more than the specified consecutive times.

***

### JSON Web Token ###

This application will be using JSON Web Token specification for stateless authentication through NancyFx piplelines.

#### What is a JSON Web Token (JWT)? ####

The web is stateless which means that data cannot be saved across requests. This means that in order to protect resources the user must be validated for every request. A common approach is to store the authentication with a session on the server. This approach, however, does not scale well horizontally. Consider what might happen if the application were to be split among multiple servers? Managing a session across multiple servers is difficult. The JWT does allow for authentication to be handled **across multiple servers**.

The JWT is a cryptographically signed and encrypted JSON object that is saved on the client and sent back to the server. The neat thing about JWT is the flexibility of the information that can used for authentication.

A developer will need to decide where to store the JWT once the user has been authenticated. This [stormpath](https://stormpath.com/blog/where-to-store-your-jwts-cookies-vs-html5-web-storage) article outlines both approaches: Cookies & Web Storage.

#### Cookies ####

Cookies could be used if the API is being served on the same domain as the application. The advantage of using cookies is that they can be set automatically from the server. The authentication cookie is then automatically sent with every subsequent request to that domain.

Cookies, with the proper HttpOnly flag, can also be protected from being accessed by JavaScript. This helps to protect against cross-site scripting attacks (XSS). There is another concern to protect against cross-site request forgery attacks. The site can generate a **csrf token** to prevent these attacks.

#### Web Storage ####

Web storage is another storage method available for storing JWT. The user can pick between localStorage or sessionStorage. The localStorage will persist until explicitly deleted and the sessionStorage will be deleted only while the window that created it is open.

The Web Storage must be used if accessing an API on a domain that is separate from the initiating resource. The drawback to using WebStorage is that no security standards are enforced (in contrast to cookies). The user must take proper measures to send JWT over HTTPS. To make things worse, since the WebStorage is accessible by JavaScript, there is valid concern for cross-scripting attacks.

A very real concern with XSS attacks is that third-party JavaScript can access the WebStorage and compromise the security. This makes identifying and protecting against XSS attacks difficult.

#### What do we do? ####

Cookies should be used websites that need higher security. The cookies will need to address the CSRF attacks but this is easier to manage by the application.

I will continue to use WebStorage for this project because it presents an interesting problem that I do not often encounter. This should, however, be thought through carefully when developing a *real* application.

In a **real world** situation, I would plan on using cookies if I were planning on shipping this as a live application. Using cookies would require me to host the API on the same domain as the SPA. I would also need to issue a CSRF token in the JWT.

This will be something to address at the of this project if time allows

**JWT Sources:**

https://jwt.io/introduction/

https://float-middle.com/json-web-tokens-jwt-vs-sessions/

https://stormpath.com/blog/where-to-store-your-jwts-cookies-vs-html5-web-storage

https://github.com/NancyFx/Nancy/blob/master/samples/Nancy.Demo.Authentication.Stateless/StatelessAuthBootstrapper.cs

***

### HTTPS / SSL ###

Https will be implemented for the API because we are sending username and password to login the record. We want this to be secure against a man in the middle attack.
